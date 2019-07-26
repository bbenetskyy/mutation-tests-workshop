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

    public interface IFileDataReader
    {
        string ReadString(string fileName);
        T ReadObject<T>(string fileName) where T : class;
    }

    public class MapBusLoader : IMapBusLoader
    {
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

    public class FileDataReader : IFileDataReader
    {
        public string ReadString(string fileName)
        {
            try
            {
                var codeBaseFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                //todo maybe we need extention here?
                var filePath = Path.Combine(codeBaseFolder, fileName).Substring(codeBaseFolder.IndexOf('C'));
                using (var reader = new StreamReader(filePath))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message} : {e.StackTrace}");
                return string.Empty;
            }
        }

        public T ReadObject<T>(string fileName) where T : class
        {
            var json = ReadString(fileName);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
