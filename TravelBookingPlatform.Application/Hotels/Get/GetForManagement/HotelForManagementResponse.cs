﻿using TravelBookingPlatform.Application.Owners.Common;

namespace TravelBookingPlatform.Application.Hotels.GetForManagement;

public class HotelForManagementResponse
{
  public Guid Id { get; init; }
  public string Name { get; init; }
  public int StarRating { get; init; }
  public OwnerResponse Owner { get; init; }
  public int NumberOfRooms { get; init; }
  public DateTime CreatedAtUtc { get; init; }
  public DateTime? ModifiedAtUtc { get; init; }
}