using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RzeszowBusCore.Models;

namespace RzeszowBusCore.Services
{
    //todo this should be moved to proper classes

    public interface IMapBusLoader
    {
        Task<List<MapBusStop>> GetMapBusStopsAsync();
    }

    public interface IBusStopLoader
    {
        Task<List<BusStopCollection>> GetBusStopsAsync();
    }

    public class MapStopLoader : IMapBusLoader
    {
        public Task<List<MapBusStop>> GetMapBusStopsAsync()
        {
            throw new NotImplementedException();
        }
    }

    public class BusStopLoader : IBusStopLoader
    {
        public Task<List<BusStopCollection>> GetBusStopsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
