using TravelBookingPlatform.Api.Dtos.Rooms;
using AutoFixture;
using FluentAssertions;
using Xunit;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using Moq;

namespace TravelBookingPlatform.Tests.Dtos;
    
public class RoomTests
{
    private readonly Fixture _fixture;

    public RoomTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ShouldValidateRequiredFields()
    {
        //Arrange
        var createRoom = _fixture.Build<Mock<RoomCreationRequest>>()
                                    .Without(x => x.Object.Number)
                                   .Create();

        //Act
        var validationResults = ValidateModel(createRoom.Object);

        //Assert
        validationResults.Should().ContainSingle(v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("required"));
    }

    [Fact]
    public void ShouldPassValidationWhenValid()
    {
        //Arrange
        var createRoom = _fixture.Create<Mock<RoomCreationRequest>>();

        //Act
        var validationResults = ValidateModel(createRoom.Object);

        //Assert
        validationResults.Should().BeEmpty();
    }

    [Fact]
    public void ShouldSerializeCorrectly()
    {
        //Arrange
        var createRoom = _fixture.Create<Mock<RoomCreationRequest>>();

        //Act
        var json = JsonConvert.SerializeObject(createRoom.Object);

        //Assert
        json.Should().Contain(createRoom.Object.Number);
    }

    [Fact]
    public void ShouldDeserializeCorrectly()
    {
        //Arrange
        var createRoom = _fixture.Create<Mock<RoomCreationRequest>>();
        var json = JsonConvert.SerializeObject(createRoom.Object);

        //Act
        var deserialized = JsonConvert.DeserializeObject<RoomCreationRequest>(json);

        //Assert
        deserialized.Should().BeEquivalentTo(createRoom.Object);
    }

    private static IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, validationContext, validationResults, true);
        return validationResults;
    }
}