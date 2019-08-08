using System;

namespace RzeszowBusCore.Tests.TestData.Models
{
    public class ConverterTestClass
    {
        public int Int { get; set; }
        public long Long { get; set; }
        public double Decimal { get; set; }
        public int Int2 { get; set; }
        public double Decimal2 { get; set; }
        public Guid Guid { get; set; }
        public string String { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public TestEnum Enum { get; set; }
        public bool Bool { get; set; }
        public bool Bool2 { get; set; }
        public InnerTestClass Class { get; set; }
    }
}