using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using RzeszowBusCore.Models;
using RzeszowBusCore.Services;

namespace RzeszowBusCore.Tests
{
    //todo Move into proper places later
    public class MapBusLoaderTests
    {
        [Theory]
        [MemberData(nameof(MapBusLoaderTests.EmptyConfigurations), MemberType = typeof(MapBusLoaderTests))]
        public void Constructor_WhenArgsIsEmpty_ThrowArgumentNullException(IConfiguration configuration)
        {
            // Arrange

            // Act
            Action action = () => new MapBusLoader(configuration);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        public static IEnumerable<object[]> EmptyConfigurations => new List<IConfiguration[]>
        {
            new [] {(IConfiguration) null},
            new IConfiguration[] {new Configuration()}
        };
    }
}
