using System.Linq;
using System.Linq.Expressions;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Infrastructure.Persistence.Helpers;

public static class IQueryableExtensions
{
    public static IQueryable<T> SortByExpression<T>(
        this IQueryable<T> queryable,
        Expression<Func<T, object>> sortExpression,
        SortOrder sortOrder)
    {
        return sortOrder == SortOrder.Ascending
                ? queryable.OrderBy(sortExpression)
                : queryable.OrderByDescending(sortExpression);
    }
}

