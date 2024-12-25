using System.Data;
using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.RoomClasses.Create;

public class CreateRoomClassCommandHandler : IRequestHandler<CreateRoomClassCommand, Guid>
{
  private readonly IAmenityRepository _amenityRepository;
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateRoomClassCommandHandler(
    IHotelRepository hotelRepository,
    IRoomClassRepository roomClassRepository,
    IAmenityRepository amenityRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
  {
    _hotelRepository = hotelRepository;
    _roomClassRepository = roomClassRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _amenityRepository = amenityRepository;
  }

  public async Task<Guid> Handle(
    CreateRoomClassCommand request,
    CancellationToken cancellationToken)
  {
    if (!await _hotelRepository.ExistsAsync(h => h.Id == request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFoundById);
    }

    if (await _roomClassRepository.ExistsAsync(
          rc => rc.HotelId == request.HotelId && rc.Name == request.Name, 
          cancellationToken))
    {
      throw new DuplicateNameException(RoomCategoryMessages.DuplicateNameInHotel);
    }

    foreach (var amenityId in request.AmenitiesIds)
    {
      if (!await _amenityRepository.ExistsAsync(a => a.Id == amenityId, cancellationToken))
      {
        throw new NotFoundException(AmenityMessages.NotFoundById);
      }
    }

    var roomClass = _mapper.Map<RoomCategory>(request);
    
    foreach (var amenityId in request.AmenitiesIds)
    {
      roomClass.Amenities
        .Add((await _amenityRepository.GetByIdAsync(amenityId, cancellationToken))!);
    }

    var createdRoomClass = await _roomClassRepository.CreateAsync(
      roomClass, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return createdRoomClass.Id;
  }
}