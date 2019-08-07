using System;
using System.Collections.Generic;
using System.Text;
using RzeszowBusCore.Models;
using RzeszowBusCore.ViewModels.Abstract;

namespace RzeszowBusCore.ViewModels
{
    public class BusStopCollectionViewModel : ITable, IModel<BusStopCollection>
    {
        public BusStopCollection Model { get; set; }

        public string[] GetColumns()
            => new[] { "Id", "Title", "Buses Count" };

        public string[] GetRow()
            => new[] { Model.Id.ToString(), Model.Title, Model.SimpleBusStops.Count.ToString() };

        public bool HaveInnerTable() => Model.SimpleBusStops.Count > 0;
    }
}
