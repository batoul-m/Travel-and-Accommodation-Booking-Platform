using TravelBookingPlatform.Api.Dtos.Auth;
using AutoFixture;
using FluentAssertions;
using Xunit;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Moq;

namespace TravelBookingPlatform.Tests.Dtos.Auth;

public class LoginRequestTests
{
    private readonly Fixture _fixture;

    public LoginRequestTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ShouldValidateRequiredFields()
    {
        // Arrange
        var mockRequest = _fixture.Build<Mock<LoginRequest>>()
                                  .Without(x => x.Object.Email)
                                  .Create();

        // Act
        var validationResults = ValidateModel(mockRequest.Object);

        // Assert
        validationResults.Should().ContainSingle(v => v.MemberNames.Contains("Email"));
    }

    [Fact]
    public void ShouldPassValidationWhenValid()
    {
        // Arrange
        var mockRequest = _fixture.Create<Mock<LoginRequest>>();

        // Act
        var validationResults = ValidateModel(mockRequest.Object);

        // Assert
        validationResults.Should().BeEmpty();
    }

    [Fact]
    public void ShouldSerializeCorrectly()
    {
        // Arrange
        var mockRequest = _fixture.Create<Mock<LoginRequest>>();

        // Act
        var json = JsonConvert.SerializeObject(mockRequest.Object);

        // Assert
        json.Should().Contain(mockRequest.Object.Email).And.Contain(mockRequest.Object.Password);
    }

    [Fact]
    public void ShouldDeserializeCorrectly()
    {
        // Arrange
        var mockRequest = _fixture.Create<Mock<LoginRequest>>();
        var json = JsonConvert.SerializeObject(mockRequest.Object);

        // Act
        var deserialized = JsonConvert.DeserializeObject<LoginRequest>(json);

        // Assert
        deserialized.Should().BeEquivalentTo(mockRequest.Object);
    }

    private static IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, validationContext, validationResults, true);
        return validationResults;
    }
}