using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssert;
using FluentAssertions;
using RzeszowBusCore.Services;
using RzeszowBusCore.Tests.TestData;
using Xunit;

namespace RzeszowBusCore.Tests.Services
{
    public class MapBusStopTrimTests
    {
        [Theory]
        [InlineData(7, 5)]
        [InlineData(6, 5)]
        [InlineData(5, 5)]
        [InlineData(5, 6)]
        [InlineData(5, 7)]
        public void Trim_TrimProperParameters_OutListHaveTrimmedValues(int stringLength, int maxLength)
        {
            // Arrange
            TrimTestData.GenerateTestData(stringLength);
            var busStopTrim = new MapBusStopTrim();
            var expectedLength = stringLength < maxLength ? stringLength : maxLength;

            // Act
            var trimList = busStopTrim.Trim(TrimTestData.MapBusStops, maxLength);

            // Assert
            trimList.ShouldNotBeNull();
            trimList.ForEach(x =>
            {
                x.Model.LongName.Length.Should().Be(expectedLength);
                x.Model.ShortName.Length.Should().Be(expectedLength);
            });
        }
    }

    public class BusStopCollectionTrimTests
    {
        [Theory]
        [InlineData(10, 5)]
        [InlineData(5, 5)]
        [InlineData(5, 10)]
        public void Trim_TrimProperParameters_OutListHaveTrimmedValues(int stringLength, int maxLength)
        {
            // Arrange
            TrimTestData.GenerateTestData(stringLength);
            var busStopTrim = new BusStopCollectionTrim();
            var expectedLength = stringLength < maxLength ? stringLength : maxLength;

            // Act
            var trimList = busStopTrim.Trim(TrimTestData.BusStopCollection, maxLength);

            // Assert
            trimList.ShouldNotBeNull();
            trimList.ForEach(x =>
            {
                x.Model.Title.Length.Should().Be(expectedLength);
                x.Model.SimpleBusStops.ForEach(y =>
                {
                    y.Name.Length.Should().Be(expectedLength);
                    y.Title.Length.Should().Be(expectedLength);
                });
            });
        }
    }
}
