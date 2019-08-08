using System.Collections.Generic;
using RzeszowBusCore.Models;
using RzeszowBusCore.Services.Abstract;
using RzeszowBusCore.Validators;
using RzeszowBusCore.ViewModels;

namespace RzeszowBusCore.Services
{
    public class BusStopCollectionTrim : ITrim<BusStopCollectionViewModel>
    {
        public List<BusStopCollectionViewModel> Trim(List<BusStopCollectionViewModel> items, int maxLength)
        {
            items.ForEach(x => Trim(x.Model, maxLength));
            return items;
        }

        private void Trim(BusStopCollection model, int maxLength)
        {
            model.Title = model.Title.TrimToMaxLength(maxLength);
            model.SimpleBusStops.ForEach(x => Trim(x, maxLength));
        }

        private void Trim(SimpleBusStop model, int maxLength)
        {
            model.Title = model.Title.TrimToMaxLength(maxLength);
            model.Name = model.Name.TrimToMaxLength(maxLength);
        }
    }
}