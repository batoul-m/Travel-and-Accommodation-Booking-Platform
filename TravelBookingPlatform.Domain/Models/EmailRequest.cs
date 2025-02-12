namespace TravelBookingPlatform.Domain.Model;

public record EmailRequest(
    IEnumerable<string> ToEmails,
    string Subject,
    string Body,
    IEnumerable<Attachment> Attachments);