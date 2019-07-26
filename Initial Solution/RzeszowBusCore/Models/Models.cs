using System;
using System.Collections.Generic;
using System.Text;

namespace RzeszowBusCore.Models
{
    //todo this should be moved to proper classes

    public class MapBusStop
    {
        public int Id { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public int Id2 { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Id3 { get; set; }
        public List<Dictionary<int,Dictionary<int,string>>> Buses { get; set; }
    }

    public class BusStopCollection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<SimpleBusStop> SimpleBusStops { get; set; }
    }

    public class SimpleBusStop
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public int Id2 { get; set; }
    }

    public interface IConfiguration
    {
        string GetBusStopList { get; }
        string GetMapBusStopList { get; }
    }

    public class Configuration: IConfiguration
    {
        public string GetBusStopList { get; set; }
        public string GetMapBusStopList { get; set; }
    }
}
