using System.Collections.Generic;
using System.Threading.Tasks;
using RzeszowBusCore.Models;
using RzeszowBusCore.ViewModels;

namespace RzeszowBusCore.Services.Abstract
{
    public interface IBusStopLoader
    {
        Task<List<BusStopCollectionViewModel>> GetBusStopsAsync();
    }
}