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
    public class BusStopCollectionViewModelTests
    {
        [Fact]
        public void GetColumns_ColumnNamesAreNotEmpty()
        {
            // Arrange
            var viewModel = new BusStopCollectionViewModel();

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
            var viewModel = new BusStopCollectionViewModel();

            // Act
            viewModel.Model = ViewModelTestData.BusStopCollection;
            var rowValues = viewModel.GetRow();

            // Assert
            rowValues[0].Should().Be(viewModel.Model.Id.ToString());
            rowValues[1].Should().Be(viewModel.Model.Title);
            rowValues[2].Should().Be(viewModel.Model.SimpleBusStops.Count.ToString());
        }

        [Theory]
        [MemberData(nameof(ViewModelTestData.BusStopCollectionWithoutStops), MemberType = typeof(ViewModelTestData))]
        public void HaveInnerTable_ModelNullOrNoStops_FalseReturned(BusStopCollection model)
        {
            // Arrange
            var viewModel = new BusStopCollectionViewModel();

            // Act
            viewModel.Model = model;
            var haveInnerTable = viewModel.HaveInnerTable();

            // Assert
            haveInnerTable.ShouldBeFalse();
        }

        [Theory]
        [MemberData(nameof(ViewModelTestData.BusStopCollectionWithStops), MemberType = typeof(ViewModelTestData))]
        public void HaveInnerTable_ModelHaveStops_TrueReturned(BusStopCollection model)
        {
            // Arrange
            var viewModel = new BusStopCollectionViewModel();

            // Act
            viewModel.Model = model;
            var haveInnerTable = viewModel.HaveInnerTable();

            // Assert
            haveInnerTable.ShouldBeTrue();
        }
    }
}
