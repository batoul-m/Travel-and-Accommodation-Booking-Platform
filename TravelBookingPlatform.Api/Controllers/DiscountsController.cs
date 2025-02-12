﻿using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Api.Dtos.Discounts;
using TravelBookingPlatform.Api.Extensions;
using TravelBookingPlatform.Application.Discounts.Create;
using TravelBookingPlatform.Application.Discounts.Delete;
using TravelBookingPlatform.Application.Discounts.Get;
using TravelBookingPlatform.Application.Discounts.GetById;
using TravelBookingPlatform.Domain;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Api.Controllers;

[ApiController]
[Route("api/room-classes/{roomClassId:guid}/discounts")]
[ApiVersion("1.0")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class DiscountsController : ControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public DiscountsController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpGet]
  [ProducesResponseType(typeof(IEnumerable<DiscountResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<IEnumerable<DiscountResponse>>> GetAmenities(
    Guid roomClassId,
    [FromQuery] DiscountsGetRequest discountsGetRequest,
    CancellationToken cancellationToken)
  {
    if (discountsGetRequest is null)
    {
      return BadRequest("Discounts request cannot be null.");
    }

    var query = new GetDiscountsQuery { RoomClassId = roomClassId };
    _mapper.Map(discountsGetRequest, query);

    var discounts = await _mediator.Send(query, cancellationToken);

    Response.Headers.AddPaginationMetadata(discounts.PaginationMetadata);

    return Ok(discounts.Items);
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet("{id:guid}")]
  public async Task<ActionResult<DiscountResponse>> GetDiscount(
    Guid roomClassId,
    Guid id,
    CancellationToken cancellationToken)
  {
    if (id == Guid.Empty)
    {
      return BadRequest("Discount ID cannot be empty.");
    }

    var query = new GetDiscountByIdQuery
    {
      RoomClassId = roomClassId,
      DiscountId = id
    };

    var discount = await _mediator.Send(query, cancellationToken);

    return Ok(discount);
  }

  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPost]
  public async Task<IActionResult> CreateDiscount(
    Guid roomClassId,
    DiscountCreationRequest discountCreationRequest,
    CancellationToken cancellationToken)
  {
    if (discountCreationRequest is null)
    {
      return BadRequest("Discount creation request cannot be null.");
    }

    var command = new CreateDiscountCommand { RoomClassId = roomClassId };
    _mapper.Map(discountCreationRequest, command);

    var createdDiscount = await _mediator.Send(command, cancellationToken);

    return CreatedAtAction(nameof(GetDiscount), new { id = createdDiscount.Id }, createdDiscount);
  }

  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteDiscount(
    Guid roomClassId,
    Guid id,
    CancellationToken cancellationToken)
  {
    if (id == Guid.Empty)
    {
      return BadRequest("Discount ID cannot be empty.");
    }

    var command = new DeleteDiscountCommand
    {
      RoomCategoryId = roomClassId,
      DiscountId = id
    };

    await _mediator.Send(command, cancellationToken);

    return NoContent();
  }
}