using System;
using System.Linq;
using ConsoleTables;
using McMaster.Extensions.CommandLineUtils;

namespace RzeszowBusApp
{
    class Program
    {
        static int Main(string[] args)
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



            var table = new ConsoleTable("one", "two", "three");
            table.AddRow(1, 2, 3)
                .AddRow("this line should be longer", "yes it is", "oh");

            table.Write();
            Console.WriteLine();

            //var rows = Enumerable.Repeat(new Something(), 10);

            //ConsoleTable
            //    .From<Something>(rows)
            //    .Configure(o => o.NumberAlignment = Alignment.Right)
            //    .Write(Format.Alternative);

            Console.ReadKey();
        }
    }
}
