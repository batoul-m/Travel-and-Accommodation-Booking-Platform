using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Api.Extensions;
using TravelBookingPlatform.Api.Dtos.Cities;
using TravelBookingPlatform.Api.Dtos.Images;
using TravelBookingPlatform.Application.Cities.Create;
using TravelBookingPlatform.Application.Cities.Delete;
using TravelBookingPlatform.Application.Cities.GetForManagement;
using TravelBookingPlatform.Application.Cities.GetTrending;
using TravelBookingPlatform.Application.Cities.SetThumbnail;
using TravelBookingPlatform.Application.Cities.Update;
using TravelBookingPlatform.Domain;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Api.Controllers;

[ApiController]
[Route("api/cities")]
[ApiVersion("1.0")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class CitiesController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public CitiesController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CityForManagementResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<CityForManagementResponse>>> GetCitiesForManagement(
        [FromQuery] CitiesGetRequest citiesGetRequest,
        CancellationToken cancellationToken)
    {
        if (citiesGetRequest is null)
        {
            return BadRequest("Cities request cannot be null.");
        }

        var query = _mapper.Map<GetCitiesForManagementQuery>(citiesGetRequest);
        var cities = await _mediator.Send(query, cancellationToken);

        Response.Headers.AddPaginationMetadata(cities.PaginationMetadata);

        return Ok(cities.Items);
    }

    [HttpGet("trending")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<TrendingCityResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TrendingCityResponse>>> GetTrendingCities(
        [FromQuery] TrendingCitiesGetRequest trendingCitiesGetRequest,
        CancellationToken cancellationToken)
    {
        if (trendingCitiesGetRequest is null)
        {
            return BadRequest("Trending cities request cannot be null.");
        }

        var query = _mapper.Map<GetTrendingCitiesQuery>(trendingCitiesGetRequest);
        var cities = await _mediator.Send(query, cancellationToken);

        return Ok(cities);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CityResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CityResponse>> CreateCity(
        [FromBody] CityCreationRequest cityCreationRequest,
        CancellationToken cancellationToken)
    {
        if (cityCreationRequest is null)
        {
            return BadRequest("City creation request cannot be null.");
        }

        var command = _mapper.Map<CreateCityCommand>(cityCreationRequest);
        var response = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetCitiesForManagement), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateCity(
        Guid id,
        [FromBody] CityUpdateRequest cityUpdateRequest,
        CancellationToken cancellationToken)
    {
        if (cityUpdateRequest is null)
        {
            return BadRequest("City update request cannot be null.");
        }

        var command = new UpdateCityCommand { CityId = id };
        _mapper.Map(cityUpdateRequest, command);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteCity(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCityCommand { CityId = id };

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPut("{id:guid}/thumbnail")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetCityThumbnail(
        Guid id,
        [FromForm] ImageCreationRequest imageCreationRequest,
        CancellationToken cancellationToken)
    {
        if (imageCreationRequest is null)
        {
            return BadRequest("Image request cannot be null.");
        }

        var command = new SetCityThumbnailCommand { CityId = id };
        _mapper.Map(imageCreationRequest, command);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}