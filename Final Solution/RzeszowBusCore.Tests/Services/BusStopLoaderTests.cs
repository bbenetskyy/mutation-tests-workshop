using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ConfigurationBuilder;
using FluentAssertions;
using RzeszowBusCore.Converters;
using RzeszowBusCore.Converters.Abstract;
using RzeszowBusCore.Models;
using RzeszowBusCore.Models.Abstract;
using RzeszowBusCore.Services;
using Xunit;

namespace RzeszowBusCore.Tests.Services
{
    public class BusStopLoaderTests
    {
        private IContainer _container;

        public BusStopLoaderTests()
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
            builder.RegisterType<BaseJsonToObjectConverter>().As<IJsonToObjectConverter>();

            _container = builder.Build();
        }

        [Theory]
        [MemberData(nameof(BusStopLoaderTests.EmptyConfigurations), MemberType = typeof(BusStopLoaderTests))]
        public void Constructor_WhenArgsIsEmpty_ThrowArgumentNullException(IConfiguration configuration)
        {
            // Arrange

            // Act
            Action action = () => new BusStopLoader(configuration, null);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .And.ParamName.Should().Be(nameof(IConfiguration.GetBusStopList));
        }

        [Fact]
        public void Constructor_WhenArgsExist_ObjectCreated()
        {
            // Arrange
            var configuration = new Configuration { GetBusStopList = nameof(Configuration.GetBusStopList) };
            BusStopLoader busLoader = null;

            // Act
            Action action = () => busLoader = new BusStopLoader(configuration, null);

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
            var busLoader = new BusStopLoader(configuration, null);

            // Assert
            busLoader.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .First(p => p.Name == "_busStopListUrl")
                .GetValue(busLoader)
                .Should().Be(configuration.GetBusStopList);
        }

        [Fact]
        public async Task GetMapBusStopsAsync_WithProductionConfiguration_MapBusStopListReturned()
        {
            // Arrange
            var configuration = _container.Resolve<IConfiguration>();
            var converter = _container.Resolve<IJsonToObjectConverter>();
            var busLoader = new BusStopLoader(configuration, converter);

            // Act
            var busStops = await busLoader.GetBusStopsAsync();

            // Assert
            busStops.Should().HaveCountGreaterThan(1);
        }

        public static IEnumerable<object[]> EmptyConfigurations => new List<IConfiguration[]>
        {
            new [] {(IConfiguration) null},
            new IConfiguration[] {new Configuration()}
        };
    }
}
