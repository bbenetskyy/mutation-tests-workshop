using System;
using System.Collections.Generic;
using System.Text;
using FluentAssert;
using FluentAssertions;
using RzeszowBusCore.Services;
using RzeszowBusCore.Tests.TestData;
using Xunit;

namespace RzeszowBusCore.Tests.Services
{
    public class MapBusStopFilterTests
    {
        [Theory]
        [InlineData("a", 1)]
        [InlineData("Lon", 1)]
        [InlineData("AmE", 1)]
        [InlineData("d", 1)]
        [InlineData("PO", 1)]
        [InlineData("q", 1)]
        [InlineData("g", 2)]
        [InlineData("f", 2)]
        [InlineData("t", 2)]
        [InlineData("o", 3)]
        [InlineData("liket", 0)]
        [InlineData("to", 0)]
        public void FilterBy_FilterAccordingToFilter_FilteredListHaveExpectedCount(string filter, int filteredCount)
        {
            // Arrange
            var busFilter = new MapBusStopFilter();

            // Act
            var filteredList = busFilter.FilterBy(FilterTestData.MapBusStops, new List<string> { filter });

            // Assert
            filteredList.ShouldNotBeNull();
            filteredList.Should().HaveCount(filteredCount);
        }
    }
}
