using System;
using System.Collections.Generic;
using System.Text;

namespace RzeszowBusCore.Models
{
    //todo this should be moved to proper classes

    public class MapBusStop : ITable
    {
        public int Id { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public int Id2 { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Id3 { get; set; }
        public Dictionary<int, string> Buses { get; set; }

        public string[] GetColumns()
            => new[] { "Id", "Long Name", "Short Name", "Longitude", "Latitude", "Buses Count" };

        public string[] GetRow()
            => new[]
            {
                Id.ToString(), LongName, ShortName, Longitude.ToString(), Latitude.ToString(),
                Buses.Keys.Count.ToString()
            };

        public bool HaveInnerTable() => Buses.Keys.Count > 0;
    }

    public class BusStopCollection : ITable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<SimpleBusStop> SimpleBusStops { get; set; }

        public string[] GetColumns()
        => new[] { "Id", "Title", "Buses Count" };


        public string[] GetRow()
            => new[] { Id.ToString(), Title, SimpleBusStops.Count.ToString() };

        public bool HaveInnerTable() => SimpleBusStops.Count > 0;
    }

    public class SimpleBusStop : ITable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public int Id2 { get; set; }

        public string[] GetColumns()
            => new[] { "Id", "Title", "Name" };

        public string[] GetRow()
            => new[] { Id.ToString(), Title, Name };

        public bool HaveInnerTable() => false;
    }

    public interface IConfiguration
    {
        string GetBusStopList { get; }
        string GetMapBusStopList { get; }
    }

    public class Configuration : IConfiguration
    {
        public string GetBusStopList { get; set; }
        public string GetMapBusStopList { get; set; }
    }

    public interface ITable
    {
        string[] GetColumns();
        string[] GetRow();
        bool HaveInnerTable();
    }
}
