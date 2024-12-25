using MediatR;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Interfaces.Services;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Bookings;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserContext _userContext;
  private readonly IUserRepository _userRepository;

  public DeleteBookingCommandHandler(
    IBookingRepository bookingRepository, 
    IUserContext userContext,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository)
  {
    _bookingRepository = bookingRepository;
    _userContext = userContext;
    _unitOfWork = unitOfWork;
    _userRepository = userRepository;
  }

  public async Task Handle(
    DeleteBookingCommand request,
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

    if (!await _bookingRepository.ExistsAsync(
          b => b.Id == request.BookingId && b.GuestId == _userContext.Id,
          cancellationToken))
    {
      throw new NotFoundException(BookingMessages.NotFoundForSpecifiedGuest);
    }

    var booking = await _bookingRepository.GetByIdAsync(
      request.BookingId, cancellationToken);

    if (booking!.CheckInDateUtc <= DateOnly.FromDateTime(DateTime.UtcNow))
    {
      throw new BadRequestException(BookingMessages.CannotCancelAfterCheckIn);
    }

    await _bookingRepository.DeleteAsync(request.BookingId, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}