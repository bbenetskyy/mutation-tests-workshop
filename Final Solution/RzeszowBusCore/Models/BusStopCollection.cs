using System.Collections.Generic;
using RzeszowBusCore.Models.Abstract;
using RzeszowBusCore.ViewModels.Abstract;

namespace RzeszowBusCore.Models
{
    public class BusStopCollection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<SimpleBusStop> SimpleBusStops { get; set; }
    }
}