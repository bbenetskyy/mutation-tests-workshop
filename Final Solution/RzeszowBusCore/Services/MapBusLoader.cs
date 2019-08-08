using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using RzeszowBusCore.Converters;
using RzeszowBusCore.Converters.Abstract;
using RzeszowBusCore.Models;
using RzeszowBusCore.Models.Abstract;
using RzeszowBusCore.Services.Abstract;
using RzeszowBusCore.ViewModels;

namespace RzeszowBusCore.Services
{
    public class MapBusLoader : IMapBusLoader
    {
        private readonly IJsonToObjectConverter _converter;
        private readonly string _mapStopListUrl;

        public MapBusLoader(IConfiguration configuration, IJsonToObjectConverter converter)
        {
            if (string.IsNullOrWhiteSpace(configuration?.GetMapBusStopList))
                throw new ArgumentNullException(nameof(IConfiguration.GetMapBusStopList));
            _converter = converter;

            _mapStopListUrl = configuration.GetMapBusStopList;
        }

        public async Task<List<MapBusStopViewModel>> GetMapBusStopsAsync()
        {
            var jsonString = await _mapStopListUrl.GetStringAsync();
            var stops = _converter.ConvertList<MapBusStop>(jsonString);
            return stops.Select(x => new MapBusStopViewModel
            {
                Model = x
            }).ToList();
        }
    }
}