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
    public class BusStopLoader : IBusStopLoader
    {
        private readonly IJsonToObjectConverter _converter;
        private readonly string _busStopListUrl;

        public BusStopLoader(IConfiguration configuration, IJsonToObjectConverter converter)
        {
            if (string.IsNullOrWhiteSpace(configuration?.GetBusStopList))
                throw new ArgumentNullException(nameof(IConfiguration.GetBusStopList));
            _converter = converter;

            _busStopListUrl = configuration.GetBusStopList;
        }

        public async Task<List<BusStopCollectionViewModel>> GetBusStopsAsync()
        {
            var jsonString = await _busStopListUrl.GetStringAsync();
            var collections = _converter.ConvertList<BusStopCollection>(jsonString);
            return collections.Select(x => new BusStopCollectionViewModel
            {
                Model = x
            }).ToList();
        }
    }
}