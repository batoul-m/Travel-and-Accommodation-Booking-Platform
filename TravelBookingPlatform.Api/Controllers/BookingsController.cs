using System.Security.Claims;
using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Api.Dtos.Bookings;
using TravelBookingPlatform.Api.Extensions;
using TravelBookingPlatform.Application.Bookings;
using TravelBookingPlatform.Application.Bookings.Common;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/user/bookings")]
    [ApiVersion("1.0")]
    [Authorize(Roles = nameof(UserRole.Guest))]
    public class BookingsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public BookingsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingResponse>> CreateBooking(
            [FromBody] BookingCreationRequest bookingCreationRequest,
            CancellationToken cancellationToken)
        {
            if (bookingCreationRequest is null)
            {
                return BadRequest("Booking request cannot be null.");
            }

            var command = _mapper.Map<CreateBookingCommand>(bookingCreationRequest);
            var createdBooking = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetBooking), new { id = createdBooking.Id }, createdBooking);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteBooking(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteBookingCommand { BookingId = id };
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpGet("{id:guid}/invoice")]
        [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FileResult>> GetInvoiceAsPdf(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetInvoiceAsPdfQuery { BookingId = id };
            var pdf = await _mediator.Send(query, cancellationToken);

            return File(pdf, "application/pdf", "invoice.pdf");
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingResponse>> GetBooking(
            Guid id, CancellationToken cancellationToken)
        {
            var query = new GetBookingByIdQuery { BookingId = id };
            var booking = await _mediator.Send(query, cancellationToken);

            return Ok(booking);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<BookingResponse>>> GetBookings(
            [FromQuery] BookingsGetRequest bookingsGetRequest,
            CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetBookingsQuery>(bookingsGetRequest);
            var bookings = await _mediator.Send(query, cancellationToken);

            Response.Headers.AddPaginationMetadata(bookings.PaginationMetadata);

            return Ok(bookings.Items);
        }
    }
}
