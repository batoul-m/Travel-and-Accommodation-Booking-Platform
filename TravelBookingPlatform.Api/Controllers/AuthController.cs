using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Api.Dtos.Auth;
using TravelBookingPlatform.Application.Users.Login;
using TravelBookingPlatform.Application.Users.Register;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Api.Controllers;

[Route("api/auth")]
[ApiController]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginRequest loginRequest,
        CancellationToken cancellationToken)
    {
        if (loginRequest is null)
        {
            return BadRequest("Login request cannot be null.");
        }

        var loginCommand = _mapper.Map<LoginCommand>(loginRequest);
        var response = await _mediator.Send(loginCommand, cancellationToken);

        return Ok(response);
    }

    [HttpPost("register-guest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterRequest registerRequest,
        CancellationToken cancellationToken)
    {
        if (registerRequest is null)
        {
            return BadRequest("Register request cannot be null.");
        }

        var registerCommand = new RegisterCommand
        {
            Role = UserRole.Guest.ToString()
        };
        _mapper.Map(registerRequest, registerCommand);

        await _mediator.Send(registerCommand, cancellationToken);

        return NoContent();
    }
}