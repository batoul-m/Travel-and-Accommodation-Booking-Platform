using AutoMapper;
using MediatR;
using TravelBookingPlatform.Application.Discounts.GetById;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Interfaces.Services;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Discounts.Create;

public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, DiscountResponse>
{
  private readonly IDiscountRepository _discountRepository;
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IDateTimeProvider _dateTimeProvider;

  public CreateDiscountCommandHandler(
    IRoomClassRepository roomClassRepository,
    IDiscountRepository discountRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper, 
    IDateTimeProvider dateTimeProvider)
  {
    _roomClassRepository = roomClassRepository;
    _discountRepository = discountRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _dateTimeProvider = dateTimeProvider;
  }

  public async Task<DiscountResponse> Handle(
    CreateDiscountCommand request,
    CancellationToken cancellationToken)
  {
    if (!await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId, cancellationToken))
    {
      throw new NotFoundException(RoomCategoryMessages.RoomDoesNotExist);
    }

    if (await _discountRepository.ExistsAsync(
          d => request.EndDateUtc >= d.StartDateUtc && request.StartDateUtc <= d.EndDateUtc,
          cancellationToken))
    {
      throw new DiscountIntervalsConflictException(DiscountMessages.OverlappingDateInterval);
    }

    var discount = _mapper.Map<Discount>(request);

    discount.CreatedAtUtc = _dateTimeProvider.GetCurrentDateTimeUtc();

    var createdDiscount = await _discountRepository.CreateAsync(
      discount,
      cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return _mapper.Map<DiscountResponse>(createdDiscount);
  }
}