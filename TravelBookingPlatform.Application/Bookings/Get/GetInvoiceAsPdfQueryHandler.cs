using MediatR;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Interfaces.Services;
using TravelBookingPlatform.Domain.Messages;
using static TravelBookingPlatform.Application.Bookings.Common.InvoiceDetailsGenerator;

namespace TravelBookingPlatform.Application.Bookings;

public class GetInvoiceAsPdfQueryHandler : IRequestHandler<GetInvoiceAsPdfQuery, byte[]>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IPdfService _pdfService;
  private readonly IUserRepository _userRepository;
  private readonly IUserContext _userContext;

  public GetInvoiceAsPdfQueryHandler(
    IBookingRepository bookingRepository,
    IPdfService pdfService,
    IUserRepository userRepository,
    IUserContext userContext)
  {
    _bookingRepository = bookingRepository;
    _pdfService = pdfService;
    _userRepository = userRepository;
    _userContext = userContext;
  }

  public async Task<byte[]> Handle(GetInvoiceAsPdfQuery request, CancellationToken cancellationToken = default)
  {
    if (!await _userRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
    {
      throw new NotFoundException(UserMessages.NotFoundById);
    }
    
    if (_userContext.Role != UserRole.Guest.ToString())
    {
      throw new InvalidRoleException(UserMessages.NotAGuest);
    }

    var booking = await _bookingRepository.GetByIdAsync(
                    _userContext.Id,
                    request.BookingId,
                    true,
                    cancellationToken) ??
                  throw new NotFoundException(BookingMessages.NotFoundForSpecifiedGuest);

    return await _pdfService.GeneratePdfFromHtmlAsync(
      GetInvoiceHtml(booking),
      cancellationToken);
  }
}