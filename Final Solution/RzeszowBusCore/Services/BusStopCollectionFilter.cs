using System.Collections.Generic;
using System.Linq;
using RzeszowBusCore.Models;
using RzeszowBusCore.Services.Abstract;
using RzeszowBusCore.ViewModels;

namespace RzeszowBusCore.Services
{
    public class BusStopCollectionFilter : IFilter<BusStopCollectionViewModel>
    {
        public List<BusStopCollectionViewModel> FilterBy(List<BusStopCollectionViewModel> items, List<string> filters)
            => items.Where(x => IsFiltered(x.Model, filters)).ToList();

        private bool IsFiltered(BusStopCollection model, List<string> filters)
        {
            filters = filters.Select(x => x.ToLower().Trim()).ToList();
            return filters.Any(f =>
                model.SimpleBusStops.Any(x => x.Name.ToLower().Contains(f)
                                              || x.Title.ToLower().Contains(f))
                || model.Title.ToLower().Contains(f));
        }
    }
}