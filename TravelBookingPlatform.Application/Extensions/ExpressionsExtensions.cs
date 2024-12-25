using System.Linq.Expressions;

namespace TravelBookingPlatform.Application.Extensions;

public static class ExpressionExtensions
{
  public static Expression<Func<T, bool>> And<T>(
    this Expression<Func<T, bool>> left,
    Expression<Func<T, bool>> right)
  {
    var andExpression = Expression.AndAlso(left.Body,
      Expression.Invoke(right, left.Parameters[0]));

    var lambda = Expression.Lambda<Func<T, bool>>(andExpression, left.Parameters);

    return lambda;
  }
}