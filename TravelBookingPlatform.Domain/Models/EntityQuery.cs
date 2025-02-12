using System.Linq.Expressions;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Domain.Model;

public record EntityQuery<TEntity>(
    Expression<Func<TEntity, bool>> Filter,
    SortOrder SortOrder,
    string? SortColumn,
    int PageNumber,
    int PageSize);
