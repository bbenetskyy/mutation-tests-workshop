﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Flurl.Http;
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

    public interface IJsonToObjectConverter
    {
        T Convert<T>(string jsonString) where T : class, new();
        List<T> ConvertList<T>(string jsonString) where T : class, new();
    }

    public class BaseJsonToObjectConverter : IJsonToObjectConverter
    {
        public T Convert<T>(string jsonString) where T : class, new()
        {
            var json = JsonConvert.DeserializeObject(jsonString) as dynamic;
            return Convert<T>(json);
        }

        public T Convert<T>(dynamic jsonNode) where T : class, new()
        {
            var @object = new T();
            var props = typeof(T).GetProperties();
            var counter = 0;
            foreach (var propValue in jsonNode)
            {
                var prop = props[counter];
                if (IsSimpleType(prop.PropertyType))
                    SimpleTypeConverter(ref @object, propValue, prop);
                else
                    CustomConverter(ref @object, propValue, prop);
                counter++;
            }

            return @object;
        }

        private void CustomConverter<T>(ref T @object, object propValue, PropertyInfo prop) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public void SimpleTypeConverter<T>(ref T @object, dynamic propValue, PropertyInfo prop) where T : class, new()
        {
            if (!prop.PropertyType.IsPrimitive)
            {
                //todo structure
                return;
            }

            //if (prop.PropertyType == typeof(Enum))
            //{
            //    return;
            //}
            //Int,String,Long...
            prop.SetValue(@object, System.Convert.ChangeType(propValue, prop.PropertyType));
        }

        private bool IsSimpleType(Type propertyType)
            => propertyType.IsValueType || propertyType == typeof(string);


        public List<T> ConvertList<T>(string jsonString) where T : class, new()
        {
            var outList = new List<T>();
            var json = JsonConvert.DeserializeObject(jsonString) as dynamic;
            foreach (var node in json)
            {
                outList.Add(Convert<T>(node));
            }

            return outList;
        }
    }

    public class MapBusLoader : IMapBusLoader
    {
        private readonly IJsonToObjectConverter _converter;
        private readonly string _mapStopListUrl;

        public MapBusLoader(IConfiguration configuration, IJsonToObjectConverter converter)
        {
            if (string.IsNullOrWhiteSpace(configuration?.GetMapBusStopList))
                throw new ArgumentNullException(nameof(IConfiguration.GetMapBusStopList));
            _converter = converter;
            //todo :
            //if (string.IsNullOrWhiteSpace(configuration.GetMapBusStopList))
            //    throw new ArgumentNullException(nameof(IConfiguration.GetMapBusStopList));

            _mapStopListUrl = configuration.GetMapBusStopList;
        }

        public async Task<List<MapBusStop>> GetMapBusStopsAsync()
        {
            try
            {
                var stop = new MapBusStop();
                var jsonString = await _mapStopListUrl.GetStringAsync();
                return _converter.ConvertList<MapBusStop>(jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return new List<MapBusStop>();
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
