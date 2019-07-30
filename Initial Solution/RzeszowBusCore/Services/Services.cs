using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
    public class MapBusLoader : IMapBusLoader
    {
        private string _mapStopListUrl;

        public MapBusLoader(IConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(configuration.GetMapBusStopList))
                throw new ArgumentNullException(nameof(IConfiguration.GetMapBusStopList));

            _mapStopListUrl = configuration.GetMapBusStopList;
        }

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
