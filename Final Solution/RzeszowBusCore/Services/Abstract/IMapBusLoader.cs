using System.Collections.Generic;
using System.Threading.Tasks;
using RzeszowBusCore.Models;
using RzeszowBusCore.ViewModels;

namespace RzeszowBusCore.Services.Abstract
{
    public interface IMapBusLoader
    {
        Task<List<MapBusStopViewModel>> GetMapBusStopsAsync();
    }
}