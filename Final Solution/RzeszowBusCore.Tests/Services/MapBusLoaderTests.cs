using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using ConfigurationBuilder;
using FluentAssert;
using FluentAssertions;
using RzeszowBusCore.Converters;
using RzeszowBusCore.Converters.Abstract;
using RzeszowBusCore.Models;
using RzeszowBusCore.Models.Abstract;
using RzeszowBusCore.Services;
using RzeszowBusCore.Services.Abstract;
using RzeszowBusCore.ViewModels;
using Xunit;

namespace RzeszowBusCore.Tests.Services
{
    public class MapBusLoaderTests
    {
        private IContainer _container;

        public MapBusLoaderTests()
        {
            var codeBaseFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var configuration = new ConfigurationBuilder<Configuration>()
                .FromFile(Path
                    .Combine(codeBaseFolder, "Configuration.json")
                    .Substring(codeBaseFolder.IndexOf('C')))
                .AsJsonFormat()
                .Build();

            var builder = new ContainerBuilder();

            builder.Register(c => configuration).As<IConfiguration>().SingleInstance();
            builder.RegisterType<MapBusJsonToObjectConverter>().As<IJsonToObjectConverter>();

            _container = builder.Build();
        }

        [Theory]
        [MemberData(nameof(MapBusLoaderTests.EmptyConfigurations), MemberType = typeof(MapBusLoaderTests))]
        public void Constructor_WhenArgsIsEmpty_ThrowArgumentNullException(IConfiguration configuration)
        {
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
        public void Constructor_WhenArgsExist_CorrectUrlSaved()
        {
            // Arrange
            var configuration = _container.Resolve<IConfiguration>();

            // Act
            var busLoader = new MapBusLoader(configuration, null);

            // Assert
            busLoader.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .First(p => p.Name == "_mapStopListUrl")
                .GetValue(busLoader)
                .Should().Be(configuration.GetMapBusStopList);
        }

        [Fact]
        public async Task GetMapBusStopsAsync_WithProductionConfiguration_MapBusStopListReturned()
        {
            // Arrange
            var configuration = _container.Resolve<IConfiguration>();
            var converter = _container.Resolve<IJsonToObjectConverter>();
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
