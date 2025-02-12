using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace TravelBookingPlatform.Application.Extensions.Validation;

public static class ValidationExtensions
{
  public static IRuleBuilderOptions<T, string> ValidName<T>(
    this IRuleBuilder<T, string> ruleBuilder, int minLength, int maxLength)
  {
    return ruleBuilder
      .Matches(@"^[A-Za-z(),\-\s]+$")
      .WithMessage(ValidationMessages.NameIsNotValid)
      .Length(minLength, maxLength);
  }

  public static IRuleBuilderOptions<T, string> PhoneNumber<T>(
    this IRuleBuilder<T, string> ruleBuilder)
  {
    return ruleBuilder
      .Matches(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")
      .WithMessage(ValidationMessages.PhoneNumberIsNotValid);
  }

  public static IRuleBuilderOptions<T, string> ValidNumericString<T>(
    this IRuleBuilder<T, string> ruleBuilder,
    int length)
  {
    return ruleBuilder
      .Matches($"^[0-9]{{{length}}}$")
      .WithMessage(ValidationMessages.GenerateNotANumericStringMessage(length));
  }

  public static IRuleBuilderOptions<T, IFormFile> ValidImage<T>(
    this IRuleBuilder<T, IFormFile> ruleBuilder)
  {
    var allowedImageTypes = new[] { "image/jpg", "image/jpeg", "image/png" };

    return ruleBuilder
      .Must(x => allowedImageTypes.Contains(x.ContentType, StringComparer.OrdinalIgnoreCase))
      .WithMessage(ValidationMessages.NotAnImageFile);
  }

  public static IRuleBuilderOptions<T, string> StrongPassword<T>(this IRuleBuilderOptions<T, string> ruleBuilder)
  {
    return ruleBuilder
      .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
      .WithMessage(ValidationMessages.PasswordIsWeak);
  }
}