using System.Collections.Generic;

namespace RzeszowBusCore.Converters
{
    public interface IJsonToObjectConverter
    {
        T Convert<T>(string jsonString) where T : class, new();
        List<T> ConvertList<T>(string jsonString) where T : class, new();
    }
}