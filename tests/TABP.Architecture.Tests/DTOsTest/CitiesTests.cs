using AutoFixture;
using TravelBookingPlatform.Api.Dtos.Cities;
using FluentAssertions;
using Xunit;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using Moq;

namespace TravelBookingPlatform.Tests.Dtos.Cities;

public class CitiesTests
    {
        private readonly Fixture _fixture;

        public CitiesTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ShouldInitializeCorrectly()
        {
            var dto = _fixture.Create<CityCreationRequest>();
            Assert.NotNull(dto);
        }

        [Fact]
        public void CityName_ShouldSetAndGetCorrectly()
        {
            var dto = new CityCreationRequest { Name = "New York" };
            Assert.Equal("New York", dto.Name);
        }
    }
