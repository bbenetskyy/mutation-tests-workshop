using System;
using Xunit;
using FluentAssertions;
using RzeszowBusCore.Models;
using RzeszowBusCore.Services;

namespace RzeszowBusCore.Tests
{
    //todo Move into proper places later
    public class MapBusLoaderTests
    {
        [Fact]
        public void Constructor_WhenArgsIsEmpty_ThrowArgumentNullException()
        {
            // Arrange

            // Act
            Action action = () => new MapBusLoader(new Configuration());

            // Assert
            action.Should().NotBeNull();
        }
    }
}
