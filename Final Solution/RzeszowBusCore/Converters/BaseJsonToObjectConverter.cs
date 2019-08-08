using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RzeszowBusCore.Converters.Abstract;

namespace RzeszowBusCore.Converters
{
    //todo this should be moved to proper classes

    public class BaseJsonToObjectConverter : IJsonToObjectConverter
    {
        public T Convert<T>(string jsonString) where T : class, new()
        {
            var json = JToken.Parse(jsonString);
            return Convert<T>(json);
        }

        private T Convert<T>(JToken jToken) where T : class, new()
        {
            var @object = new T();
            if (jToken.Type != JTokenType.Array) return @object;

            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
            {
                var prop = typeof(T).GetProperties()[2];
                foreach (var propValue in jToken)
                {
                    CustomConverter(ref @object, propValue, prop);
                }
            }
            else
            {
                var counter = 0;
                var props = typeof(T).GetProperties();
                foreach (var propValue in jToken)
                {
                    var prop = props[counter];
                    if (IsSimpleType(prop.PropertyType))
                        SimpleTypeConverter(ref @object, propValue, prop);
                    else
                        CustomConverter(ref @object, propValue, prop);
                    counter++;
                }
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
            var value = genericMethod?.Invoke(this, new object[] { valueToken });
            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
            {
                typeof(T).GetMethod("Add")?.Invoke(@object, new[] { value });
            }
            else
            {
                prop.SetValue(@object, value);
            }
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
}
