using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using RzeszowBusCore.Models;
using RzeszowBusCore.Services;
using ConfigurationBuilder;

namespace RzeszowBusCore.Tests
{
    public class MapBusLoaderTests
    {
        [Theory]
        [MemberData(nameof(MapBusLoaderTests.EmptyConfigurations), MemberType = typeof(MapBusLoaderTests))]
        public void Constructor_WhenArgsIsEmpty_ThrowArgumentNullException(IConfiguration configuration)
        {
            // Arrange

            // Act
            Action action = () => new MapBusLoader(configuration, null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_WhenArgsExist_ObjectCreated()
        {
            // Arrange
            var configuration = new Configuration { GetMapBusStopList = nameof(Configuration.GetMapBusStopList) };
            MapBusLoader busLoader = null;

            // Act
            Action action = () => busLoader = new MapBusLoader(configuration, null);

            // Assert
            action.Should().NotThrow<Exception>();
            busLoader.Should().NotBeNull();
        }

        [Fact]
        public async Task GetMapBusStopsAsync_WithProductionConfiguration_MapBusStopListReturned()
        {
            // Arrange
            var codeBaseFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var configuration = new ConfigurationBuilder<Configuration>()
                .FromFile(Path
                    .Combine(codeBaseFolder, "Configuration.json")
                    .Substring(codeBaseFolder.IndexOf('C')))
                .AsJsonFormat()
                .Build();
            //todo add IoC here in tests
            var converter = new BaseJsonToObjectConverter();
            var busLoader = new MapBusLoader(configuration, converter);

            // Act
            var mapBusStops = await busLoader.GetMapBusStopsAsync();

            // Assert
            mapBusStops.Should().HaveCountGreaterThan(1);
        }

        public static IEnumerable<object[]> EmptyConfigurations => new List<IConfiguration[]>
        {
            new [] {(IConfiguration) null},
            new IConfiguration[] {new Configuration()}
        };
    }
}
