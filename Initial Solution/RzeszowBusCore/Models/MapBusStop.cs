using System;
using System.Collections.Generic;
using System.Text;
using RzeszowBusCore.Models.Abstract;
using RzeszowBusCore.ViewModels.Abstract;

namespace RzeszowBusCore.Models
{
    public class MapBusStop
    {
        public int Id { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public int Id2 { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Id3 { get; set; }
        public Dictionary<int, string> Buses { get; set; }

    }
}
