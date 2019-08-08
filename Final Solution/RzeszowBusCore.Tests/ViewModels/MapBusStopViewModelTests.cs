using System;
using System.Collections.Generic;
using System.Text;
using FluentAssert;
using FluentAssertions;
using RzeszowBusCore.Models;
using RzeszowBusCore.Tests.TestData;
using RzeszowBusCore.ViewModels;
using Xunit;

namespace RzeszowBusCore.Tests.ViewModels
{
    public class MapBusStopViewModelTests
    {
        [Fact]
        public void GetColumns_ColumnNamesAreNotEmpty()
        {
            // Arrange
            var viewModel = new MapBusStopViewModel();

            // Act
            var columns = viewModel.GetColumns();

            // Assert
            foreach (var colName in columns)
            {
                colName.Should().NotBeNullOrEmpty();
            }
        }

        [Fact]
        public void GetRow_ModelValuesReturned()
        {
            // Arrange
            var viewModel = new MapBusStopViewModel();

            // Act
            viewModel.Model = ViewModelTestData.MapBusStop;
            var rowValues = viewModel.GetRow();

            // Assert
            rowValues[0].Should().Be(viewModel.Model.Id.ToString());
            rowValues[1].Should().Be(viewModel.Model.LongName);
            rowValues[2].Should().Be(viewModel.Model.ShortName);
            rowValues[3].Should().Be(viewModel.Model.Longitude.ToString());
            rowValues[4].Should().Be(viewModel.Model.Latitude.ToString());
            rowValues[5].Should().Be(viewModel.Model.Buses.Keys.Count.ToString());
        }

        [Theory]
        [MemberData(nameof(ViewModelTestData.MapBusStopsWithoutBusses), MemberType = typeof(ViewModelTestData))]
        public void HaveInnerTable_ModelNullOrNoBusses_FalseReturned(MapBusStop model)
        {
            // Arrange
            var viewModel = new MapBusStopViewModel();

            // Act
            viewModel.Model = model;
            var haveInnerTable = viewModel.HaveInnerTable();

            // Assert
            haveInnerTable.ShouldBeFalse();
        }

        [Theory]
        [MemberData(nameof(ViewModelTestData.MapBusStopsWithBusses), MemberType = typeof(ViewModelTestData))]
        public void HaveInnerTable_ModelHaveBusses_TrueReturned(MapBusStop model)
        {
            // Arrange
            var viewModel = new MapBusStopViewModel();

            // Act
            viewModel.Model = model;
            var haveInnerTable = viewModel.HaveInnerTable();

            // Assert
            haveInnerTable.ShouldBeTrue();
        }
    }
}
