using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using ConfigurationBuilder;
using ConsoleTables;
using McMaster.Extensions.CommandLineUtils;
using RzeszowBusCore.Converters;
using RzeszowBusCore.Models;
using RzeszowBusCore.Models.Abstract;
using RzeszowBusCore.Services;
using RzeszowBusCore.Services.Abstract;

namespace RzeszowBusApp
{
    class Program
    {
        static IContainer Container { get; set; }

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

            app.OnExecute(async () =>
            {
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


            //var table = new ConsoleTable("one", "two", "three");
            //table.AddRow(1, 2, 3)
            //    .AddRow("this line should be longer", "yes it is", "oh");

            //table.Write();
            //Console.WriteLine();

            //var rows = Enumerable.Repeat(new Something(), 10);

            //ConsoleTable
            //    .From<Something>(rows)
            //    .Configure(o => o.NumberAlignment = Alignment.Right)
            //    .Write(Format.Alternative);

        }

        static async Task PrintBusStops()
        {
            var busStopLoader = Container.Resolve<IBusStopLoader>();
            var busStops = await busStopLoader.GetBusStopsAsync();

            PrintResults(busStops);
        }

        static async Task PrintMapBusStops()
        {
            var busStopLoader = Container.Resolve<IMapBusLoader>();
            var busStops = await busStopLoader.GetMapBusStopsAsync();

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

            return builder.Build();
        }
    }
}
