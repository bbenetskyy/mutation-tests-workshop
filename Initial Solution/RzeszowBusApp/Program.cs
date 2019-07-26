using System;
using System.Linq;
using Autofac;
using ConsoleTables;
using McMaster.Extensions.CommandLineUtils;
using RzeszowBusCore.Models;
using RzeszowBusCore.Services;

namespace RzeszowBusApp
{
    class Program
    {
        static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            //var app = new CommandLineApplication
            //{
            //    Name = "asasasd.dll",
            //    ThrowOnUnexpectedArgument = false
            //};

            //app.HelpOption("-h|--help");

            //var optionBefore = app.Option<int>("-b|--before <N>", "asdasd", CommandOptionType.SingleValue);
            //var optionAfter = app.Option<int>("-a|--after <N>", "asdasdasd", CommandOptionType.SingleValue);
            //var optionFilter = app.Option<bool>("-f|--filter <B>", "asascascascas", CommandOptionType.SingleValue);

            //app.OnExecute(async () =>
            //{
            //    //var yourApp = new YourApp();
            //});

            //return app.Execute(args);


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

            Container = ProductionBuild();
            var config = Container.Resolve<IConfiguration>();
        }

        static IContainer ProductionBuild()
        {

            var builder = new ContainerBuilder();

            builder.RegisterType<FileDataReader>().As<IFileDataReader>();
            builder.RegisterType<MapBusLoader>().As<IMapBusLoader>();
            builder.RegisterType<BusStopLoader>().As<IBusStopLoader>();

            builder.RegisterType<FileDataReader>().As<IFileDataReader>().SingleInstance();
            builder.Register(c=>c.Resolve<IFileDataReader>().ReadObject<Configuration>("Configuration.json"))
                .As<IConfiguration>().SingleInstance();

            return builder.Build();
        }
    }
}
