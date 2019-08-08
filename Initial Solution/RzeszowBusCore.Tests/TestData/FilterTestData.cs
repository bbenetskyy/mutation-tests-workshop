using System;
using System.Collections.Generic;
using System.Text;
using RzeszowBusCore.Models;
using RzeszowBusCore.ViewModels;

namespace RzeszowBusCore.Tests.TestData
{
    public static class FilterTestData
    {
        public static List<MapBusStopViewModel> MapBusStops { get; }
        public static List<BusStopCollectionViewModel> BusStopCollection { get; }

        static FilterTestData()
        {
            MapBusStops = new List<MapBusStopViewModel>
            {
                new MapBusStopViewModel
                {
                    Model = new MapBusStop
                    {
                        Buses = new Dictionary<int, string>
                        {
                            {1, "a"},
                            {2, "b"},
                            {3, "c"},
                        },
                        LongName = "LongF",
                        ShortName = "Name"
                    }
                },
                new MapBusStopViewModel
                {
                    Model = new MapBusStop
                    {
                        Buses = new Dictionary<int, string>
                        {
                            {1, "d"},
                            {2, "3"},
                            {3, "f"},
                        },
                        LongName = "Poi",
                        ShortName = "Kol"
                    }
                },
                new MapBusStopViewModel
                {
                    Model = new MapBusStop
                    {
                        Buses = new Dictionary<int, string>
                        {
                            {1, "t"},
                            {2, "g"},
                            {3, "m"},
                        },
                        LongName = "123",
                        ShortName = "456"
                    }
                },
                new MapBusStopViewModel
                {
                    Model = new MapBusStop
                    {
                        Buses = new Dictionary<int, string>
                        {
                            {1, "q"},
                            {2, "w"},
                            {3, "t"},
                        },
                        LongName = "Like",
                        ShortName = "OOT"
                    }
                }
            };

            BusStopCollection = new List<BusStopCollectionViewModel>
            {
                new BusStopCollectionViewModel
                {
                    Model = new BusStopCollection
                    {
                        Title = "LongF",
                        SimpleBusStops = new List<SimpleBusStop>
                        {
                            new SimpleBusStop
                            {
                                Name = "Name",
                                Title = "a"
                            },

                            new SimpleBusStop
                            {
                                Name = "b",
                                Title = "c"
                            }
                        }
                    }
                },
                new BusStopCollectionViewModel
                {
                    Model = new BusStopCollection
                    {
                        Title = "Kol",
                        SimpleBusStops = new List<SimpleBusStop>
                        {
                            new SimpleBusStop
                            {
                                Name = "d",
                                Title = "3"
                            },

                            new SimpleBusStop
                            {
                                Name = "Poi",
                                Title = "f"
                            }
                        }
                    }
                },
                new BusStopCollectionViewModel
                {
                    Model = new BusStopCollection
                    {
                        Title = "t",
                        SimpleBusStops = new List<SimpleBusStop>
                        {
                            new SimpleBusStop
                            {
                                Name = "g",
                                Title = "m"
                            },

                            new SimpleBusStop
                            {
                                Name = "123",
                                Title = "456"
                            }
                        }
                    }
                },
                new BusStopCollectionViewModel
                {
                    Model = new BusStopCollection
                    {
                        Title = "q",
                        SimpleBusStops = new List<SimpleBusStop>
                        {
                            new SimpleBusStop
                            {
                                Name = "w",
                                Title = "t"
                            },

                            new SimpleBusStop
                            {
                                Name = "Like",
                                Title = "OOT"
                            }
                        }
                    }
                }
            };
        }
    }
}
