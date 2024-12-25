using TravelBookingPlatform.Api.Dtos.Bookings;
using AutoFixture;
using FluentAssertions;
using Xunit;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using Moq;
using System;

namespace TravelBookingPlatform.Tests.Dtos;
public class BookingCreationRequestTests
{
    private readonly Fixture _fixture;

    public BookingCreationRequestTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ShouldValidateRequiredFields()
    {
        // Arrange
        var mockRequest = _fixture.Build<Mock<BookingCreationRequest>>()
                                  .Without(x => x.Object.CheckInDateUtc)
                                  .Without(x => x.Object.CheckOutDateUtc)
                                  .Without(x => x.Object.RoomIds)
                                  .Create();

        // Act
        var validationResults = ValidateModel(mockRequest.Object);

        // Assert
        validationResults.Should().ContainSingle(v => v.MemberNames.Contains("StartDate"));
        validationResults.Should().ContainSingle(v => v.MemberNames.Contains("EndDate"));
        validationResults.Should().ContainSingle(v => v.MemberNames.Contains("GuestId"));
    }

    [Fact]
    public void ShouldPassValidationWhenValid()
    {
    // Arrange
    var mockRequest = _fixture.Create<Mock<BookingCreationRequest>>();

    // Act
    var validationResults = ValidateModel(mockRequest.Object);

    // Assert
    validationResults.Should().BeEmpty();
    }

    [Fact]
    public void ShouldSerializeCorrectly()
    {
        // Arrange
        var mockRequest = _fixture.Create<Mock<BookingCreationRequest>>();

        // Act
        var json = JsonConvert.SerializeObject(mockRequest.Object);

        // Assert
        json.Should().Contain(mockRequest.Object.CheckInDateUtc.ToString())
            .And.Contain(mockRequest.Object.CheckOutDateUtc.ToString())
            .And.Contain(mockRequest.Object.RoomIds.ToString());
    }

    [Fact]
    public void ShouldDeserializeCorrectly()
    {
        // Arrange
        var mockRequest = _fixture.Create<Mock<BookingCreationRequest>>();
        var json = JsonConvert.SerializeObject(mockRequest.Object);

        // Act
        var deserialized = JsonConvert.DeserializeObject<BookingCreationRequest>(json);

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