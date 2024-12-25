using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Interfaces.Services;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Application.Reviews.Update;

namespace TravelBookingPlatform.Application.Reviews.Update;

public class UpdateReviewCommandHandler : IRequest<UpdateReviewCommand>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;
  private readonly IReviewRepository _reviewRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;
  private readonly IUserContext _userContext;

  public UpdateReviewCommandHandler(
    IHotelRepository hotelRepository,
    IUserRepository userRepository,
    IReviewRepository reviewRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper, 
    IUserContext userContext)
  {
    _hotelRepository = hotelRepository;
    _userRepository = userRepository;
    _reviewRepository = reviewRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _userContext = userContext;
  }

  public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
  {
    if (!await _hotelRepository.ExistsAsync(h => h.Id == request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFoundById);
    }

    if (!await _userRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
    {
      throw new NotFoundException(UserMessages.NotFoundById);
    }
    
    if (_userContext.Role != UserRole.Guest.ToString())
    {
      throw new InvalidRoleException(UserMessages.NotAGuest);
    }

    var review = await _reviewRepository.GetByIdAsync(request.ReviewId,
                   request.HotelId, _userContext.Id, cancellationToken)
                 ?? throw new NotFoundException(ReviewMessages.NotFoundForUserAndHotel);

    var ratingSum = await _reviewRepository.GetTotalRatingForHotelAsync(request.HotelId, cancellationToken);

    var reviewsCount = await _reviewRepository.GetReviewCountForHotelAsync(request.HotelId, cancellationToken);

    ratingSum += request.Rating - review.Rating;

    var newRating = 1.0 * ratingSum / reviewsCount;

    await _hotelRepository.UpdateReviewById(request.HotelId,
      newRating, cancellationToken);

    await _reviewRepository.UpdateAsync(
      _mapper.Map(request, review), cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}