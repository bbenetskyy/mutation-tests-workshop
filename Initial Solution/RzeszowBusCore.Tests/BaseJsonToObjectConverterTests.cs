using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssert;
using FluentAssertions;
using Newtonsoft.Json;
using RzeszowBusCore.Services;
using RzeszowBusCore.Tests.TestData;
using Xunit;

namespace RzeszowBusCore.Tests
{
    public class BaseJsonToObjectConverterTests
    {
        [Fact]
        public void Test()
        {
            // Arrange
            var converter = new BaseJsonToObjectConverter();
            var filePath = "./TestData/TestClass.json";

            // Act
            var jsonString = File.ReadAllText(filePath);
            var testClass = converter.Convert<ConverterTestClass>(jsonString);

            // Assert
            testClass.Should().BeEquivalentTo(ConverterTestData.TestClass);
        }
    }

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

    public class InnerTestClass
    {
        public int Int { get; set; }
    }

    public enum TestEnum
    {
        Fake, Test
    }
}
