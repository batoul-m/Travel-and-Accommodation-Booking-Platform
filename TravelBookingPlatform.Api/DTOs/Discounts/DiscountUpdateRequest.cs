﻿using MediatR;

namespace TravelBookingPlatform.Api.Dtos.Discounts;

public class DiscountUpdateRequest : IRequest
{
  public decimal Percentage { get; init; }
  public DateTime StartDateUtc { get; init; }
  public DateTime EndDateUtc { get; init; }
}