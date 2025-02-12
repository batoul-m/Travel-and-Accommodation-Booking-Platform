using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Application.Amenities;
using TravelBookingPlatform.Application.Amenities.Create;
using TravelBookingPlatform.Api.Dtos.Amenities;
using TravelBookingPlatform.Domain;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Api.Extensions;

namespace TravelBookingPlatform.Api.Controllers;

[ApiController]
[Route("api/amenities")]
[ApiVersion("1.0")]
public class AmenitiesController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AmenitiesController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AmenityResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<AmenityResponse>>> GetAmenities(
        [FromQuery] AmenitiesGetRequest amenitiesGetRequest,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
          return BadRequest(ModelState);
        }

        var query = _mapper.Map<GetAmenitiesQuery>(amenitiesGetRequest);

        try
        {
            var amenities = await _mediator.Send(query, cancellationToken);
            Response.Headers.AddPaginationMetadata(amenities.PaginationMetadata);
            return Ok(amenities.Items);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AmenityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AmenityResponse>> GetAmenity(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetAmenityByIdQuery { AmenityId = id };
            var amenity = await _mediator.Send(query, cancellationToken);

            if (amenity == null)
                return NotFound();

            return Ok(amenity);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAmenity(
        [FromBody] AmenityCreationRequest amenityCreationRequest,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var command = _mapper.Map<CreateAmenity>(amenityCreationRequest);
            var createdAmenity = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetAmenity), new { id = createdAmenity.Id }, createdAmenity);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateAmenity(
        Guid id,
        [FromBody] AmenityUpdateRequest amenityUpdateRequest,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var command = new UpdateAmenity { AmenityId = id };
            _mapper.Map(amenityUpdateRequest, command);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}