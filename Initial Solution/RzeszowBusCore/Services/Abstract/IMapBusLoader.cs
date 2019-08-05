using System.Collections.Generic;
using System.Threading.Tasks;
using RzeszowBusCore.Models;

namespace RzeszowBusCore.Services.Abstract
{
    public interface IMapBusLoader
    {
        Task<List<MapBusStop>> GetMapBusStopsAsync();
    }
}