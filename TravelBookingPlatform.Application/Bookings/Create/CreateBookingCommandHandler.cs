using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Interfaces.Services;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Domain.Model;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Application.Bookings.Common;
using static TravelBookingPlatform.Application.Bookings.BookingEmail;
using static TravelBookingPlatform.Application.Bookings.Common.InvoiceDetailsGenerator;

namespace TravelBookingPlatform.Application.Bookings;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IDateTimeProvider _dateTimeProvider;
  private readonly IEmailService _emailService;
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;
  private readonly IPdfService _pdfService;
  private readonly IRoomRepository _roomRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;
  private readonly IUserContext _userContext;

  public CreateBookingCommandHandler(
    IHotelRepository hotelRepository,
    IRoomRepository roomRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IBookingRepository bookingRepository,
    IEmailService emailService,
    IUserContext userContext,
    IPdfService pdfService,
    IDateTimeProvider dateTimeProvider, 
    IUserRepository userRepository)
  {
    _hotelRepository = hotelRepository;
    _roomRepository = roomRepository;
    _mapper = mapper;
    _unitOfWork = unitOfWork;
    _bookingRepository = bookingRepository;
    _emailService = emailService;
    _userContext = userContext;
    _pdfService = pdfService;
    _dateTimeProvider = dateTimeProvider;
    _userRepository = userRepository;
  }

  public async Task<BookingResponse> Handle(
    CreateBookingCommand request,
    CancellationToken cancellationToken = default)
  {
    if (!await _userRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
    {
      throw new NotFoundException(UserMessages.NotFoundById);
    }
    
    if (_userContext.Role != UserRole.Guest.ToString())
    {
      throw new InvalidRoleException(UserMessages.NotAGuest);
    }

    var hotel = await _hotelRepository.GetByIdAsync(
                  request.HotelId,
                  false, false, false,
                  cancellationToken)
                ?? throw new NotFoundException(HotelMessages.NotFoundById);

    await _unitOfWork.BeginTransactionAsync(cancellationToken);

    try
    {
      var rooms = await ValidateRooms(
        request.RoomIds,
        request.HotelId,
        request.CheckInDateUtc,
        request.CheckOutDateUtc,
        cancellationToken);
      
      var booking = new Booking
      {
        Hotel = hotel,
        Rooms = rooms,
        GuestId = _userContext.Id,
        CheckInDateUtc = request.CheckInDateUtc,
        CheckOutDateUtc = request.CheckOutDateUtc,
        TotalPrice = CalculateTotalPrice(rooms,
          request.CheckInDateUtc, request.CheckOutDateUtc),
        BookingDateUtc = _dateTimeProvider.GetCurrentDateUtc(),
        GuestRemarks = request.GuestRemarks,
        PaymentMethod = request.PaymentMethod
      };

      var createdBooking = await _bookingRepository.CreateAsync(
        booking, cancellationToken);

      foreach (var room in rooms)
      {
        var invoiceRecord = new InvoiceRecord
        {
          Booking = createdBooking,
          RoomCategoryName = room.RoomCategory.Name,
          RoomNumber = room.Number,
          PriceAtBooking = room.RoomCategory.PricePerNight,
          DiscountPercentageAtBooking = room.RoomCategory.Discounts.FirstOrDefault()?.Percentage
        };

        createdBooking.Invoice.Add(invoiceRecord);
      }
      
      await _unitOfWork.SaveChangesAsync(cancellationToken);

      var invoicePdf = await _pdfService.GeneratePdfFromHtmlAsync(
        GetInvoiceHtml(createdBooking),
        cancellationToken);

      var invoiceAttachment = new Attachment(
        "invoice.pdf",
        invoicePdf,
        "application",
        "pdf");

      await _emailService.SendAsync(
        GetBookingEmailRequest(
          _userContext.Email,
           new[]{invoiceAttachment}
        ), cancellationToken);

      await _unitOfWork.CommitTransactionAsync(cancellationToken);

      return _mapper.Map<BookingResponse>(createdBooking);
    }
    catch
    {
      await _unitOfWork.RollbackTransactionAsync(cancellationToken);
      throw;
    }
  }

  private async Task<List<Room>> ValidateRooms(
    IEnumerable<Guid> roomIds,
    Guid hotelId,
    DateOnly checkInDate,
    DateOnly checkOutDate,
    CancellationToken cancellationToken = default)
  {
    var rooms = new List<Room>();

    foreach (var roomId in roomIds)
    {
      var room = await _roomRepository.GetByIdWithRoomClassAsync(roomId, cancellationToken)
                 ?? throw new NotFoundException(RoomMessages.NotFoundById);

      if (room.RoomCategory.HotelId != hotelId)
      {
        throw new RoomDoesNotBelongToHotelException(RoomMessages.NotInSameHotel);
      }

      if (!await _roomRepository.ExistsAsync(
            r => r.RoomId == roomId &&
                 r.Bookings.All(
                   b => checkInDate >= b.CheckOutDateUtc || 
                        checkOutDate <= b.CheckInDateUtc), 
            cancellationToken))
      {
        throw new RoomUnavailableException(RoomMessages.NotAvailable(roomId));
      }

      rooms.Add(room);
    }

    return rooms;
  }

  private static decimal CalculateTotalPrice(IEnumerable<Room> rooms, DateOnly requestCheckInDate,
    DateOnly requestCheckOutDate)
  {
    var totalPricePerNight = rooms
      .Sum(r => r.RoomCategory.PricePerNight *
        (100 - (r.RoomCategory.Discounts.FirstOrDefault()?.Percentage ?? 0)) / 100);

    var bookingDuration = requestCheckOutDate.DayNumber - requestCheckInDate.DayNumber;

    return totalPricePerNight * bookingDuration;
  }
}