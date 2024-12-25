using AutoMapper;
using MediatR;
using TravelBookingPlatform.Application.Reviews.Common;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Reviews.GetByHotelId;

public class GetReviewsByHotelIdQueryHandler : IRequestHandler<GetReviewsByHotelIdQuery, PaginatedResult<ReviewResponse>>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;
  private readonly IReviewRepository _reviewRepository;

  public GetReviewsByHotelIdQueryHandler(
    IReviewRepository reviewRepository,
    IMapper mapper,
    IHotelRepository hotelRepository)
  {
    _reviewRepository = reviewRepository;
    _mapper = mapper;
    _hotelRepository = hotelRepository;
  }

  public async Task<PaginatedResult<ReviewResponse>> Handle(
    GetReviewsByHotelIdQuery request,
    CancellationToken cancellationToken)
  {
    if (!await _hotelRepository.ExistsAsync(h => h.Id == request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFoundById);
    }

    var query = new EntityQuery<Review>(
      r => r.HotelId == request.HotelId,
      request.SortOrder ?? SortOrder.Ascending,
      request.SortColumn,
      request.PageNumber,
      request.PageSize);

    var owners = await _reviewRepository.GetAsync(query,
      cancellationToken);

    return _mapper.Map<PaginatedResult<ReviewResponse>>(owners);
  }
}