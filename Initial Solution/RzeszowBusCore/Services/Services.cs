using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            //var json = JsonConvert.DeserializeObject(jsonString) as dynamic;
            var json = JToken.Parse(jsonString);
            return Convert<T>(json);
        }

        private T Convert<T>(JToken jToken) where T : class, new()
        {
            var @object = new T();
            var props = typeof(T).GetProperties();
            var counter = 0;

            if (jToken.Type != JTokenType.Array) return @object;

            foreach (var propValue in jToken)
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

        protected virtual void CustomConverter<T>(ref T @object, JToken valueToken, PropertyInfo prop)
            where T : class, new()
        {
            if (valueToken.Type != JTokenType.Array) return;

            var type = GetType();
            var method = type.GetMethod("Convert", BindingFlags.NonPublic | BindingFlags.Instance);
            var genericMethod = method?.MakeGenericMethod(prop.PropertyType);
            var value = genericMethod?.Invoke(this, new[] {valueToken});
            prop.SetValue(@object, value);
        }

        private void SimpleTypeConverter<T>(ref T @object, JToken valueToken, PropertyInfo prop) where T : class, new()
        {
            switch (valueToken.Type)
            {
                case JTokenType.Integer:
                case JTokenType.Float:
                    prop.SetValue(@object, System.Convert.ChangeType(valueToken, prop.PropertyType));
                    break;
                case JTokenType.String:
                    if (prop.PropertyType.IsEnum)
                    {
                        prop.SetValue(@object, Enum.Parse(prop.PropertyType, valueToken.ToString(), true));
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        prop.SetValue(@object, valueToken.Value<string>());
                    }
                    else if (prop.PropertyType == typeof(DateTime))
                    {
                        prop.SetValue(@object, System.Convert.ToDateTime(valueToken));
                    }
                    else if (prop.PropertyType == typeof(TimeSpan))
                    {
                        prop.SetValue(@object, System.Convert.ToDateTime(valueToken).TimeOfDay);
                    }
                    else if (prop.PropertyType == typeof(Guid))
                    {
                        prop.SetValue(@object, Guid.Parse(valueToken.Value<string>()));
                    }
                    else
                    {
                        //Int, Long, Float, Bool...
                        prop.SetValue(@object, System.Convert.ChangeType(valueToken, prop.PropertyType));
                    }
                    break;
                case JTokenType.Boolean:
                    if (prop.PropertyType == typeof(bool))
                    {
                        prop.SetValue(@object, valueToken.Value<bool>());
                    }
                    break;
                case JTokenType.Date:
                    if (prop.PropertyType == typeof(DateTime))
                    {
                        prop.SetValue(@object, valueToken.Value<DateTime>());
                    }
                    break;
                case JTokenType.Guid:
                    if (prop.PropertyType == typeof(Guid))
                    {
                        prop.SetValue(@object, valueToken.Value<Guid>());
                    }
                    break;
                case JTokenType.TimeSpan:
                    if (prop.PropertyType == typeof(TimeSpan))
                    {
                        prop.SetValue(@object, valueToken.Value<TimeSpan>());
                    }
                    break;
            }
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
