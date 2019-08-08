using System;
using System.Collections.Generic;
using System.Text;
using RzeszowBusCore.Models;
using RzeszowBusCore.ViewModels.Abstract;

namespace RzeszowBusCore.ViewModels
{
    public class MapBusStopViewModel : ITable, IModel<MapBusStop>
    {
        public MapBusStop Model { get; set; }

        public string[] GetColumns()
            => new[] { "Id", "Long Name", "Short Name", "Longitude", "Latitude", "Buses Count" };

        public string[] GetRow()
            => new[]
            {
                Model?.Id.ToString(), Model?.LongName, Model?.ShortName, Model?.Longitude.ToString(), Model?.Latitude.ToString(),
                Model?.Buses.Keys.Count.ToString()
            };

        public bool HaveInnerTable() => Model?.Buses?.Keys.Count > 0;
    }
}
