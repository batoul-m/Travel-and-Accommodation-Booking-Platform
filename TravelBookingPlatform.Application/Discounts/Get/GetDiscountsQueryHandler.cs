using AutoMapper;
using MediatR;
using TravelBookingPlatform.Application.Discounts.GetById;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Discounts.Get;

public class GetDiscountsQueryHandler : IRequestHandler<GetDiscountsQuery, PaginatedResult<DiscountResponse>>
{
  private readonly IDiscountRepository _discountRepository;
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;

  public GetDiscountsQueryHandler(
    IRoomClassRepository roomClassRepository, 
    IDiscountRepository discountRepository,
    IMapper mapper)
  {
    _roomClassRepository = roomClassRepository;
    _discountRepository = discountRepository;
    _mapper = mapper;
  }

  public async Task<PaginatedResult<DiscountResponse>> Handle(
    GetDiscountsQuery request,
    CancellationToken cancellationToken = default)
  {
    if (!await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId, cancellationToken))
    {
      throw new NotFoundException(RoomCategoryMessages.RoomDoesNotExist);
    }

    var query = new EntityQuery<Discount>(
      d => d.RoomClassId == request.RoomClassId,
      request.SortOrder ?? SortOrder.Ascending,
      request.SortColumn,
      request.PageNumber,
      request.PageSize);

    var owners = await _discountRepository.GetAsync(query, cancellationToken);

    return _mapper.Map<PaginatedResult<DiscountResponse>>(owners);
  }
}