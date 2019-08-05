using System;
using System.Collections.Generic;
using System.Text;
using RzeszowBusCore.Models.Abstract;

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
}
