using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Interfaces.Services;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Domain.Model;
using TravelBookingPlatform.Application.Bookings.Common;

namespace TravelBookingPlatform.Application.Bookings;

public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, PaginatedResult<BookingResponse>>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IMapper _mapper;
  private readonly IUserRepository _userRepository;
  private readonly IUserContext _userContext;

  public GetBookingsQueryHandler(
    IUserRepository userRepository,
    IBookingRepository bookingRepository,
    IMapper mapper,
    IUserContext userContext)
  {
    _userRepository = userRepository;
    _bookingRepository = bookingRepository;
    _mapper = mapper;
    _userContext = userContext;
  }

  public async Task<PaginatedResult<BookingResponse>> Handle(GetBookingsQuery request,
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

    var query = new EntityQuery<Booking>(
      b => b.GuestId == _userContext.Id,
      request.SortOrder ?? SortOrder.Ascending,
      request.SortColumn,
      request.PageNumber,
      request.PageSize);

    var bookings = await _bookingRepository.GetAsync(query, cancellationToken);

    return _mapper.Map<PaginatedResult<BookingResponse>>(bookings);
  }
}