using TravelBookingPlatform.Api.Dtos.RoomClasses;
using AutoFixture;
using FluentAssertions;
using Xunit;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using Moq;

namespace TravelBookingPlatform.Tests.Dtos;

public class RoomClassTests
{
    private readonly Fixture _fixture;

    public RoomClassTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ShouldReturnValidationErrorWhenTypeIsMissing()
    {
        // Arrange
        var createRoomClass = _fixture.Build<Mock<RoomClassCreationRequest>>()
                                   .Without(x => x.Object.Name)
                                   .Create();

        // Act
        var validationResults = ValidateModel(createRoomClass.Object);

        // Assert
        validationResults.Should().ContainSingle(v => v.MemberNames.Contains("Type") && v.ErrorMessage.Contains("required"));
    }

    [Fact]
    public void ShouldPassValidationWhenAllFieldsAreValid()
    {
        // Arrange
        var createRoomClass = _fixture.Create<Mock<RoomClassCreationRequest>>();

        // Act
        var validationResults = ValidateModel(createRoomClass.Object);

        // Assert
        validationResults.Should().BeEmpty();
    }

    [Fact]
    public void ShouldContainTypeFieldWhenSerialized()
    {
        // Arrange
        var createRoomClass = _fixture.Create<Mock<RoomClassCreationRequest>>();

        // Act
        var json = JsonConvert.SerializeObject(createRoomClass.Object);

        // Assert
        json.Should().Contain(createRoomClass.Object.Name);
    }

    [Fact]
    public void Deserialize_ShouldMatchOriginalObject_WhenDeserialized()
    {
        // Arrange
        var createRoomClass = _fixture.Create<Mock<RoomClassCreationRequest>>();
        var json = JsonConvert.SerializeObject(createRoomClass.Object);

        // Act
        var deserialized = JsonConvert.DeserializeObject<RoomClassCreationRequest>(json);

        // Assert
        deserialized.Should().BeEquivalentTo(createRoomClass.Object);
    }

    private static IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, validationContext, validationResults, true);
        return validationResults;
    }
}