using System;
using System.Collections.Generic;
using System.Text;

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
                Long = 100,
                String = "String"
            };
        }
    }
}
