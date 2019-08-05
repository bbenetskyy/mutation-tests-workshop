using RzeszowBusCore.Models.Abstract;

namespace RzeszowBusCore.Models
{
    public class Configuration : IConfiguration
    {
        public string GetBusStopList { get; set; }
        public string GetMapBusStopList { get; set; }
    }
}