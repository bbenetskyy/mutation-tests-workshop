using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using ConfigurationBuilder;
using ConsoleTables;
using McMaster.Extensions.CommandLineUtils;
using RzeszowBusCore.Converters;
using RzeszowBusCore.Converters.Abstract;
using RzeszowBusCore.Models;
using RzeszowBusCore.Models.Abstract;
using RzeszowBusCore.Services;
using RzeszowBusCore.Services.Abstract;
using RzeszowBusCore.ViewModels;
using RzeszowBusCore.ViewModels.Abstract;

namespace RzeszowBusApp
{
    class Program
    {
        private static IContainer Container;
        private static List<string> Filters = new List<string>();
        private static int MaxLength;
        static bool TrimEnabled => MaxLength > 0;

        static int Main(string[] args)
        {
            Container = ProductionBuild();
            var app = new CommandLineApplication
            {
                Name = "BusApp.dll",
                ThrowOnUnexpectedArgument = false
            };

            app.HelpOption("-h|--help");

            var getAllBusStops = app.Option("-b|--bus-stops", "Get Bus Stops", CommandOptionType.NoValue);
            var getAllMapBusStops = app.Option("-m|--map-bus-stops", "Get Map Bus Stops", CommandOptionType.NoValue);
            var filter = app.Option("-f|--filter", "Filter By Stop", CommandOptionType.MultipleValue);
            var trim = app.Option("-t|--trim", "Cut Output Values", CommandOptionType.SingleValue);

            app.OnExecute(async () =>
            {
                if (filter.HasValue())
                {
                    Filters.AddRange(filter.Values);
                }

                if (trim.HasValue())
                {
                    int.TryParse(trim.Value(), out MaxLength);
                }

                if (getAllBusStops.HasValue())
                {
                    await PrintBusStops();
                }

                if (getAllMapBusStops.HasValue())
                {
                    await PrintMapBusStops();
                }
            });

            return app.Execute(args);
        }

        static async Task PrintBusStops()
        {
            var busStopLoader = Container.Resolve<IBusStopLoader>();
            var busStops = await busStopLoader.GetBusStopsAsync();

            if (Filters.Count != 0)
            {
                var busStopFilter = Container.Resolve<IFilter<BusStopCollectionViewModel>>();
                busStops = busStopFilter.FilterBy(busStops, Filters);
            }

            if (TrimEnabled)
            {
                var busStopFilter = Container.Resolve<ITrim<BusStopCollectionViewModel>>();
                busStops = busStopFilter.Trim(busStops, MaxLength);
            }

            PrintResults(busStops);
        }

        static async Task PrintMapBusStops()
        {
            var busStopLoader = Container.Resolve<IMapBusLoader>();
            var busStops = await busStopLoader.GetMapBusStopsAsync();

            if (Filters.Count != 0)
            {
                var busStopFilter = Container.Resolve<IFilter<MapBusStopViewModel>>();
                busStops = busStopFilter.FilterBy(busStops, Filters);
            }

            if (TrimEnabled)
            {
                var busStopFilter = Container.Resolve<ITrim<MapBusStopViewModel>>();
                busStops = busStopFilter.Trim(busStops, MaxLength);
            }

            PrintResults(busStops);
        }

        private static void PrintResults<T>(List<T> list) where T : ITable
        {
            if (list == null || list.Count == 0) return;
            var table = new ConsoleTable(list[0].GetColumns());
            list.ForEach(x => table.AddRow(x.GetRow()));
            table.Write();
        }

        static IContainer ProductionBuild()
        {
            var codeBaseFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var config = new ConfigurationBuilder<Configuration>()
                .FromFile(Path
                    .Combine(codeBaseFolder, "Configuration.json")
                    .Substring(codeBaseFolder.IndexOf('C')))
                .AsJsonFormat()
                .Build();

            var builder = new ContainerBuilder();

            builder.RegisterType<MapBusLoader>().As<IMapBusLoader>()
                .WithParameter((pi, c) => pi.ParameterType == typeof(IJsonToObjectConverter),
                    (pi, c) => new MapBusJsonToObjectConverter());

            builder.RegisterType<BusStopLoader>().As<IBusStopLoader>()
                .WithParameter((pi, c) => pi.ParameterType == typeof(IJsonToObjectConverter),
                    (pi, c) => new BaseJsonToObjectConverter());

            builder.Register(c => config).As<IConfiguration>().SingleInstance();

            builder.RegisterType<MapBusStopTrim>().As<ITrim<MapBusStopViewModel>>();
            builder.RegisterType<MapBusStopFilter>().As<IFilter<MapBusStopViewModel>>();

            builder.RegisterType<BusStopCollectionTrim>().As<ITrim<BusStopCollectionViewModel>>();
            builder.RegisterType<BusStopCollectionFilter>().As<IFilter<BusStopCollectionViewModel>>();

            return builder.Build();
        }
    }
}
