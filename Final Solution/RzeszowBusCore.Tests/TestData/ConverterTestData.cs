using System;
using System.Collections.Generic;
using System.Text;
using RzeszowBusCore.Tests.TestData.Models;

namespace RzeszowBusCore.Tests.TestData
{
    public static class ConverterTestData
    {
        public static ConverterTestClass TestClass { get; }

        static ConverterTestData()
        {
            TestClass = new ConverterTestClass
            {
                DateTime = Convert.ToDateTime("7/31/2019"),
                TimeSpan = Convert.ToDateTime("12:00").TimeOfDay,
                Enum = TestEnum.Test,
                Int = 10,
                Int2 = 101,
                Long = 100,
                Decimal = 1.1,
                Decimal2 = 1.1,
                Guid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                String = "String",
                Bool = true,
                Bool2 = true,
                Class = new InnerTestClass
                {
                    Int = 11
                }
            };
        }
    }
}
