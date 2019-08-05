using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using RzeszowBusCore.Converters;
using RzeszowBusCore.Models;
using RzeszowBusCore.Models.Abstract;
using RzeszowBusCore.Services.Abstract;

namespace RzeszowBusCore.Services
{
    public class MapBusLoader : IMapBusLoader
    {
        private readonly IJsonToObjectConverter _converter;
        private readonly string _mapStopListUrl;

        public MapBusLoader(IConfiguration configuration, IJsonToObjectConverter converter)
        {
            if (string.IsNullOrWhiteSpace(configuration?.GetMapBusStopList))
                throw new ArgumentNullException("GetMapBusStopList");
            _converter = converter;

            _mapStopListUrl = configuration.GetMapBusStopList;
        }

        public async Task<List<MapBusStop>> GetMapBusStopsAsync()
        {
            try
            {
                var jsonString = await _mapStopListUrl.GetStringAsync();
                return _converter.ConvertList<MapBusStop>(jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}