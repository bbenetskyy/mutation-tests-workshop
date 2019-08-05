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
    public class BusStopLoader : IBusStopLoader
    {
        private readonly IJsonToObjectConverter _converter;
        private readonly string _busStopListUrl;

        public BusStopLoader(IConfiguration configuration, IJsonToObjectConverter converter)
        {
            if (string.IsNullOrWhiteSpace(configuration?.GetBusStopList))
                throw new ArgumentNullException("GetBusStopList");
            _converter = converter;

            _busStopListUrl = configuration.GetBusStopList;
        }

        public async Task<List<BusStopCollection>> GetBusStopsAsync()
        {
            try
            {
                var jsonString = await _busStopListUrl.GetStringAsync();
                return _converter.ConvertList<BusStopCollection>(jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}