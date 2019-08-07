using System;
using System.Collections.Generic;
using System.Text;
using RzeszowBusCore.Models;
using RzeszowBusCore.ViewModels.Abstract;

namespace RzeszowBusCore.ViewModels
{
    public class SimpleBusStopViewModel : ITable, IModel<SimpleBusStop>
    {
        public SimpleBusStop Model { get; set; }

        public string[] GetColumns()
            => new[] { "Id", "Title", "Name" };

        public string[] GetRow()
            => new[] { Model.Id.ToString(), Model.Title, Model.Name };

        public bool HaveInnerTable() => false;
    }
}
