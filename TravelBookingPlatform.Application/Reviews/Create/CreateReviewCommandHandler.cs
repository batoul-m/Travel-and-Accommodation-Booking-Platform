using AutoMapper;
using MediatR;
using TravelBookingPlatform.Application.Reviews.Common;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Interfaces.Services;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Reviews.Create;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewResponse>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;
  private readonly IReviewRepository _reviewRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;
  private readonly IUserContext _userContext;

  public CreateReviewCommandHandler(
    IHotelRepository hotelRepository,
    IUserRepository userRepository,
    IReviewRepository reviewRepository,
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper, 
    IUserContext userContext)
  {
    _hotelRepository = hotelRepository;
    _userRepository = userRepository;
    _reviewRepository = reviewRepository;
    _bookingRepository = bookingRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _userContext = userContext;
  }

  public async Task<ReviewResponse> Handle(CreateReviewCommand request,
    CancellationToken cancellationToken = default)
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

    if (!await _bookingRepository.ExistsAsync(
          b => b.HotelId == request.HotelId && b.GuestId == _userContext.Id, 
          cancellationToken))
    {
      throw new GuestHasNoHotelBookingException(BookingMessages.NoBookingInHotelForGuest);
    }

    if (await _reviewRepository.ExistsAsync(
          r => r.GuestId == _userContext.Id && r.HotelId == request.HotelId, 
          cancellationToken))
    {
      throw new BadRequestException(ReviewMessages.GuestHasAlreadyReviewedHotel);
    }

    var ratingSum = await _reviewRepository.GetTotalRatingForHotelAsync(request.HotelId, cancellationToken);

    var reviewsCount = await _reviewRepository.GetReviewCountForHotelAsync(request.HotelId, cancellationToken);

    ratingSum += request.Rating;
    reviewsCount++;

    var newRating = 1.0 * ratingSum / reviewsCount;

    await _hotelRepository.UpdateReviewById(request.HotelId, newRating, cancellationToken);

    var createdReview = await _reviewRepository
      .CreateAsync(_mapper.Map<Review>(request), cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return _mapper.Map<ReviewResponse>(createdReview);
  }
}