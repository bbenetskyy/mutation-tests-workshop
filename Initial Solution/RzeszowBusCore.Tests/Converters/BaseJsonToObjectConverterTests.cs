using System.IO;
using FluentAssertions;
using RzeszowBusCore.Converters;
using RzeszowBusCore.Tests.TestData;
using RzeszowBusCore.Tests.TestData.Models;
using Xunit;

namespace RzeszowBusCore.Tests.Converters
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
