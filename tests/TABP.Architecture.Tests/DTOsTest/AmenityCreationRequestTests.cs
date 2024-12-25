using AutoFixture;
using TravelBookingPlatform.Api.Dtos.Amenities;
using FluentAssertions;
using Xunit;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using Moq;

namespace TravelBookingPlatform.Tests.Dtos.Amenities;

public class AmenityCreationRequestTests
{
    private readonly Fixture _fixture;

    public AmenityCreationRequestTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ShouldValidateRequiredFields()
    {
        // Arrange
        var mockRequest = _fixture.Build<Mock<AmenityCreationRequest>>()
                                    .Without(x => x.Object.Name)
                                    .Create();

        // Act
        var validationResults = ValidateModel(mockRequest.Object);

        // Assert
        validationResults.Should().ContainSingle(v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("required"));
    }

    [Fact]
    public void ShouldPassValidationWhenValid()
    {
        // Arrange
        var mockRequest = _fixture.Create<Mock<AmenityCreationRequest>>();

        // Act
        var validationResults = ValidateModel(mockRequest.Object);

        // Assert
        validationResults.Should().BeEmpty();
    }

    [Fact]
    public void ShouldSerializeCorrectly()
    {
        // Arrange
        var mockRequest = _fixture.Create<Mock<AmenityCreationRequest>>();

        // Act
        var json = JsonConvert.SerializeObject(mockRequest.Object);

        // Assert
        json.Should().Contain(mockRequest.Object.Name).And.Contain(mockRequest.Object.Description);
    }

    [Fact]
    public void ShouldDeserializeCorrectly()
    {
        // Arrange
        var mockRequest = _fixture.Create<Mock<AmenityCreationRequest>>();
        var json = JsonConvert.SerializeObject(mockRequest.Object);

        // Act
        var deserialized = JsonConvert.DeserializeObject<AmenityCreationRequest>(json);

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