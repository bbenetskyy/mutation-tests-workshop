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
using RzeszowBusCore.Models;
using RzeszowBusCore.Services;

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

            var getAllBusStops = app.Option("-bs|--bus-stops", "Get Bus Stops", CommandOptionType.NoValue);

            app.OnExecute(async () =>
            {
                if (getAllBusStops.HasValue())
                {
                    await PrintBusStops();
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

        private static void PrintResults<T>(List<T> list) where T : class
        {
            var table = new ConsoleTable(typeof(T).GetProperties().Select(x => x.Name).ToArray());
            list.ForEach(x => table.AddRow(list));
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

            builder.RegisterType<MapBusLoader>().As<IMapBusLoader>();
            builder.RegisterType<BusStopLoader>().As<IBusStopLoader>();
            builder.Register(c => config).As<IConfiguration>().SingleInstance();

            return builder.Build();
        }
    }
}
