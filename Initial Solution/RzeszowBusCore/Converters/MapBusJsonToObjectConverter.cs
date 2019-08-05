using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using RzeszowBusCore.Models;

namespace RzeszowBusCore.Converters
{
    public class MapBusJsonToObjectConverter : BaseJsonToObjectConverter
    {
        protected override void CustomConverter<T>(ref T @object, JToken valueToken, PropertyInfo prop)
        {
            if (prop.PropertyType != typeof(Dictionary<int, string>) || !(@object is MapBusStop busStopObject))
            {
                base.CustomConverter(ref @object, valueToken, prop);
                return;
            }

            var tokenList = GetDictionaryArray(valueToken);

            busStopObject.Buses = new Dictionary<int, string>();

            for (var i = 0; i < tokenList.Count; i += 2)
            {
                busStopObject.Buses.Add(tokenList[i].Value<int>(), tokenList[i + 1].Value<string>());
            }
        }

        private List<JToken> GetDictionaryArray(JToken valueToken)
        {
            try
            {
                return valueToken[0][1].Children().ToList();
            }
            catch
            {
                return new List<JToken>();
            }
        }
    }
}