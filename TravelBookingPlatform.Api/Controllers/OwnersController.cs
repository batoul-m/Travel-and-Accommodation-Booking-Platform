using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Api.Dtos.Owners;
using TravelBookingPlatform.Api.Extensions;
using TravelBookingPlatform.Application.Owners.Common;
using TravelBookingPlatform.Application.Owners.Create;
using TravelBookingPlatform.Application.Owners.Get;
using TravelBookingPlatform.Application.Owners.GetById;
using TravelBookingPlatform.Application.Owners.Update;
using TravelBookingPlatform.Domain;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Api.Controllers;

[ApiController]
[Route("api/owners")]
[Authorize(Roles = nameof(UserRole.Admin))]
[ApiVersion("1.0")]
public class OwnersController : ControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public OwnersController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<OwnerResponse>>> GetOwners(
    [FromQuery] OwnersGetRequest ownersGetRequest,
    CancellationToken cancellationToken)
  {
    var query = _mapper.Map<GetOwnersQuery>(ownersGetRequest);

    var owners = await _mediator.Send(query, cancellationToken);

    Response.Headers.AddPaginationMetadata(owners.PaginationMetadata);

    return Ok(owners.Items);
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet("{id:guid}")]
  public async Task<ActionResult<OwnerResponse>> GetOwner(Guid id,
    CancellationToken cancellationToken)
  {
    var query = new GetOwnerByIdQuery { OwnerId = id };

    var owner = await _mediator.Send(query, cancellationToken);

    return Ok(owner);
  }

  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [HttpPost]
  public async Task<IActionResult> CreateOwner(
    OwnerCreationRequest ownerCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = _mapper.Map<CreateOwnerCommand>(ownerCreationRequest);

    var createdOwner = await _mediator.Send(command, cancellationToken);

    return CreatedAtAction(nameof(GetOwner), new { id = createdOwner.Id }, createdOwner);
  }

  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateOwner(Guid id, OwnerUpdateRequest ownerUpdateRequest, 
    CancellationToken cancellationToken)
  {
    var command = new UpdateOwnerCommand { OwnerId = id };
    _mapper.Map(ownerUpdateRequest, command);

    await _mediator.Send(command, cancellationToken);

    return NoContent();
  }
}