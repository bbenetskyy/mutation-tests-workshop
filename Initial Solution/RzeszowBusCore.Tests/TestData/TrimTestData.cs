using System.Collections.Generic;
using RzeszowBusCore.Models;
using RzeszowBusCore.ViewModels;

namespace RzeszowBusCore.Tests.TestData
{
    public static class TrimTestData
    {
        public static List<MapBusStopViewModel> MapBusStops { get; private set; }
        public static List<BusStopCollectionViewModel> BusStopCollection { get; private set; }

        public static void GenerateTestData(int stringLength)
        {
            var testString = new string('*', stringLength);

            MapBusStops = new List<MapBusStopViewModel>
            {
                new MapBusStopViewModel
                {
                    Model = new MapBusStop
                    {
                        Buses = new Dictionary<int, string>
                        {
                            {1, testString},
                            {2, testString},
                            {3, testString},
                        },
                        LongName = testString,
                        ShortName = testString
                    }
                },
                new MapBusStopViewModel
                {
                    Model = new MapBusStop
                    {
                        Buses = new Dictionary<int, string>
                        {
                            {1, testString},
                            {2, testString},
                            {3, testString},
                        },
                        LongName = testString,
                        ShortName = testString
                    }
                },
                new MapBusStopViewModel
                {
                    Model = new MapBusStop
                    {
                        Buses = new Dictionary<int, string>
                        {
                            {1, testString},
                            {2, testString},
                            {3, testString},
                        },
                        LongName = testString,
                        ShortName = testString
                    }
                },
                new MapBusStopViewModel
                {
                    Model = new MapBusStop
                    {
                        Buses = new Dictionary<int, string>
                        {
                            {1,testString},
                            {2,testString},
                            {3,testString},
                        },
                        LongName = testString,
                        ShortName = testString
                    }
                }
            };

            BusStopCollection = new List<BusStopCollectionViewModel>
            {
                new BusStopCollectionViewModel
                {
                    Model = new BusStopCollection
                    {
                        Title = testString,
                        SimpleBusStops = new List<SimpleBusStop>
                        {
                            new SimpleBusStop
                            {
                                Name = testString,
                                Title = testString
                            },

                            new SimpleBusStop
                            {
                                Name = testString,
                                Title = testString
                            }
                        }
                    }
                },
                new BusStopCollectionViewModel
                {
                    Model = new BusStopCollection
                    {
                        Title = testString,
                        SimpleBusStops = new List<SimpleBusStop>
                        {
                            new SimpleBusStop
                            {
                                Name = testString,
                                Title = testString
                            },

                            new SimpleBusStop
                            {
                                Name = testString,
                                Title = testString
                            }
                        }
                    }
                },
                new BusStopCollectionViewModel
                {
                    Model = new BusStopCollection
                    {
                        Title = testString,
                        SimpleBusStops = new List<SimpleBusStop>
                        {
                            new SimpleBusStop
                            {
                                Name =testString,
                                Title = testString
                            },

                            new SimpleBusStop
                            {
                                Name = testString,
                                Title = testString
                            }
                        }
                    }
                },
                new BusStopCollectionViewModel
                {
                    Model = new BusStopCollection
                    {
                        Title = testString,
                        SimpleBusStops = new List<SimpleBusStop>
                        {
                            new SimpleBusStop
                            {
                                Name = testString,
                                Title = testString
                            },

                            new SimpleBusStop
                            {
                                Name = testString,
                                Title = testString
                            }
                        }
                    }
                }
            };
        }
    }
}