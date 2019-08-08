using FluentAssert;
using FluentAssertions;
using RzeszowBusCore.Tests.TestData;
using RzeszowBusCore.ViewModels;
using Xunit;

namespace RzeszowBusCore.Tests.ViewModels
{
    public class SimpleBusStopViewModelTests
    {
        [Fact]
        public void GetColumns_ColumnNamesAreNotEmpty()
        {
            // Arrange
            var viewModel = new SimpleBusStopViewModel();

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
            var viewModel = new SimpleBusStopViewModel();

            // Act
            viewModel.Model = ViewModelTestData.SimpleBusStop;
            var rowValues = viewModel.GetRow();

            // Assert
            rowValues[0].Should().Be(viewModel.Model.Id.ToString());
            rowValues[1].Should().Be(viewModel.Model.Title);
            rowValues[2].Should().Be(viewModel.Model.Name);
        }

        [Fact]
        public void HaveInnerTable_FalseReturned()
        {
            // Arrange
            var viewModel = new SimpleBusStopViewModel();

            // Act
            var haveInnerTable = viewModel.HaveInnerTable();

            // Assert
            haveInnerTable.ShouldBeFalse();
        }
    }
}