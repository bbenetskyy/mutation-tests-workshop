using System;
using System.Collections.Generic;
using System.Text;
using RzeszowBusCore.Models;
using RzeszowBusCore.ViewModels;

namespace RzeszowBusCore.Tests.TestData
{
    public static class ViewModelTestData
    {
        public static MapBusStop MapBusStop { get; }
        public static SimpleBusStop SimpleBusStop { get; }
        public static BusStopCollection BusStopCollection { get; }

        public static IEnumerable<object[]> MapBusStopsWithoutBusses => new List<object[]>
        {
            new object[]  { null },
            new object[]  { new MapBusStop
            {
                Buses = null
            }},
            new object[]  { new MapBusStop
            {
                Buses = new Dictionary<int, string>()
            }}
        };

        public static IEnumerable<object[]> MapBusStopsWithBusses => new List<object[]>
        {
            new object[]  { new MapBusStop
            {
                Buses = new Dictionary<int, string>
                {
                    { 1, "as" }
                }
            }},
            new object[]  { new MapBusStop
            {
                Buses = new Dictionary<int, string>
                {
                    { 1, "as" },
                    { 2, "asd" }
            }
            }}
        };

        public static IEnumerable<object[]> BusStopCollectionWithoutStops => new List<object[]>
        {
            new object[]  { null },
            new object[]  { new BusStopCollection
            {
                SimpleBusStops = null
            }},
            new object[]  { new BusStopCollection
            {
                SimpleBusStops = new List<SimpleBusStop>()
            }}
        };

        public static IEnumerable<object[]> BusStopCollectionWithStops => new List<object[]>
        {
            new object[]  { new BusStopCollection
            {
                SimpleBusStops = new List<SimpleBusStop>
                {
                    SimpleBusStop
                }
            }},
            new object[]  { new BusStopCollection
            {
                SimpleBusStops = new List<SimpleBusStop>
                {
                    SimpleBusStop, SimpleBusStop, SimpleBusStop
                }
            }}
        };

        static ViewModelTestData()
        {
            MapBusStop = new MapBusStop
            {
                LongName = "Long",
                ShortName = "Short",
                Id = 1,
                Id2 = 2,
                Id3 = 3,
                Latitude = 10.2,
                Longitude = -10.2,
                Buses = new Dictionary<int, string>
                {
                    { 1, "as" },
                    { 2, "asd" }
                }
            };

            SimpleBusStop = new SimpleBusStop
            {
                Id = 1,
                Id2 = 2,
                Name = "Name",
                Title = "Title"
            };

            BusStopCollection = new BusStopCollection
            {
                Id = 1,
                Title = "Large Title",
                SimpleBusStops = new List<SimpleBusStop>
                {
                    SimpleBusStop, SimpleBusStop, SimpleBusStop
                }
            };
        }
    }
}
