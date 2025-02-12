using System.Text.Json;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Api.Extensions;

public static class ResponseHeadersExtensions
{
  public static void AddPaginationMetadata(this IHeaderDictionary headers,
    PaginationMetadata paginationMetadata)
  {
    headers["x-pagination"] = JsonSerializer.Serialize(paginationMetadata);
  }
}