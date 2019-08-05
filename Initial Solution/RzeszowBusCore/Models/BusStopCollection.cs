using System.Collections.Generic;
using RzeszowBusCore.Models.Abstract;

namespace RzeszowBusCore.Models
{
    public class BusStopCollection : ITable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<SimpleBusStop> SimpleBusStops { get; set; }

        public string[] GetColumns()
            => new[] { "Id", "Title", "Buses Count" };


        public string[] GetRow()
            => new[] { Id.ToString(), Title, SimpleBusStops.Count.ToString() };

        public bool HaveInnerTable() => SimpleBusStops.Count > 0;
    }
}