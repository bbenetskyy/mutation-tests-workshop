namespace RzeszowBusCore.Models.Abstract
{
    public interface IConfiguration
    {
        string GetBusStopList { get; }
        string GetMapBusStopList { get; }
    }
}