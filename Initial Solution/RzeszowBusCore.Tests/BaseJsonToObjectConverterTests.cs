using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssert;
using FluentAssertions;
using Newtonsoft.Json;
using RzeszowBusCore.Converters;
using RzeszowBusCore.Services;
using RzeszowBusCore.Tests.TestData;
using Xunit;

namespace RzeszowBusCore.Tests
{
    public class BaseJsonToObjectConverterTests
    {
        [Fact]
        public void Convert_LoadClassFromJson_ConvertedTestClassReturned()
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
}
