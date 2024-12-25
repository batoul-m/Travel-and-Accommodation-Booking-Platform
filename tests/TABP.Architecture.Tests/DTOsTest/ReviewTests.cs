using TravelBookingPlatform.Api.Dtos.Reviews;
using AutoFixture;
using FluentAssertions;
using Xunit;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using Moq;

namespace TravelBookingPlatform.Tests.Dtos;
 
public class ReviewTests
{
    private readonly Fixture _fixture;

    public ReviewTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ShouldReturnValidationErrorWhenRatingIsMissing()
    {
        // Arrange
        var createReview = _fixture.Build<Mock<ReviewCreationRequest>>()
                                   .Without(x => x.Object.Rating)
                                   .Create();

        // Act
        var validationResults = ValidateModel(createReview.Object);

        // Assert
        validationResults.Should().ContainSingle(v => v.MemberNames.Contains("Rating") && v.ErrorMessage.Contains("required"));
    }

    [Fact]
    public void ShouldPassValidationWhenAllFieldsAreValid()
    {
        // Arrange
        var createReview = _fixture.Create<Mock<ReviewCreationRequest>>();

        // Act
        var validationResults = ValidateModel(createReview.Object);

        // Assert
        validationResults.Should().BeEmpty();
    }

    [Fact]
    public void ShouldContainRatingFieldWhenSerialized()
    {
        // Arrange
        var createReview = _fixture.Create<Mock<ReviewCreationRequest>>();

        // Act
        var json = JsonConvert.SerializeObject(createReview.Object);

        // Assert
        json.Should().Contain(createReview.Object.Rating.ToString());
    }

    [Fact]
    public void ShouldMatchOriginalObjectWhenDeserialized()
    {
        // Arrange
        var createReview = _fixture.Create<Mock<ReviewCreationRequest>>();
        var json = JsonConvert.SerializeObject(createReview.Object);

        // Act
        var deserialized = JsonConvert.DeserializeObject<ReviewCreationRequest>(json);

        // Assert
        deserialized.Should().BeEquivalentTo(createReview.Object);
    }

    private static IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, validationContext, validationResults, true);
        return validationResults;
    }
}