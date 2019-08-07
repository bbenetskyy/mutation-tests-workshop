using System.Collections.Generic;
using RzeszowBusCore.Models;
using RzeszowBusCore.Services.Abstract;
using RzeszowBusCore.Validators;
using RzeszowBusCore.ViewModels;

namespace RzeszowBusCore.Services
{
    public class MapBusStopTrim : ITrim<MapBusStopViewModel>
    {
        public List<MapBusStopViewModel> Trim(List<MapBusStopViewModel> items, int maxLength)
        {
            items.ForEach(x => Trim(x.Model, maxLength));
            return items;
        }

        private void Trim(MapBusStop model, int maxLength)
        {
            model.LongName = model.LongName.TrimToMaxLength(maxLength);
            model.ShortName = model.ShortName.TrimToMaxLength(maxLength);
        }
    }
}