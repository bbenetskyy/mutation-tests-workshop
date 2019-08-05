using System.Collections.Generic;
using System.Threading.Tasks;
using RzeszowBusCore.Models;

namespace RzeszowBusCore.Services.Abstract
{
    public interface IBusStopLoader
    {
        Task<List<BusStopCollection>> GetBusStopsAsync();
    }
}