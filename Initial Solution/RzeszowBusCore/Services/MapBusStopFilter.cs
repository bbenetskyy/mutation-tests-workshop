using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RzeszowBusCore.Models;
using RzeszowBusCore.Services.Abstract;
using RzeszowBusCore.ViewModels;

namespace RzeszowBusCore.Services
{
    public class MapBusStopFilter : IFilter<MapBusStopViewModel>
    {
        public List<MapBusStopViewModel> FilterBy(List<MapBusStopViewModel> items, List<string> filters)
            => items.Where(x => IsFiltered(x.Model, filters)).ToList();

        private bool IsFiltered(MapBusStop model, List<string> filters)
        {
            filters = filters.Select(x => x.ToLower().Trim()).ToList();
            return filters.Any(f =>
                model.Buses.Any(x => x.Value.ToLower().Contains(f))
                || model.LongName.ToLower().Contains(f)
                || model.ShortName.ToLower().Contains(f));
        }
    }
}
