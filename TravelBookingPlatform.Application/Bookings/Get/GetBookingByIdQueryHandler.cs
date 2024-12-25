using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Interfaces.Services;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Application.Bookings.Common;

namespace TravelBookingPlatform.Application.Bookings;

public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingResponse>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IMapper _mapper;
  private readonly IUserRepository _userRepository;
  private readonly IUserContext _userContext;

  public GetBookingByIdQueryHandler(
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

  public async Task<BookingResponse> Handle(
    GetBookingByIdQuery request,
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

    var booking = await _bookingRepository.GetByIdAsync(
                    _userContext.Id,
                    request.BookingId,
                    false,
                    cancellationToken) ??
                  throw new NotFoundException(BookingMessages.NotFoundForSpecifiedGuest);

    return _mapper.Map<BookingResponse>(booking);
  }
}