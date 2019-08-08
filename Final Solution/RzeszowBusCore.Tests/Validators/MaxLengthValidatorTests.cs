using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using RzeszowBusCore.Tests.TestData;
using RzeszowBusCore.Validators;
using Xunit;

namespace RzeszowBusCore.Tests.Validators
{
    public class MaxLengthValidatorTests
    {
        [Theory]
        [MemberData(nameof(MaxLengthValidatorTestData.TextWithMaxLenghAndExpectedResult), MemberType = typeof(MaxLengthValidatorTestData))]
        public void TrimToMaxLength_Should_Check_If_Text_Contains_Only_Digits(string text, int maxLength, string expectedResult)
        {
            //Act
            var trimmedText = text.TrimToMaxLength(maxLength);

            //Assert
            trimmedText.Length.Should().BeLessOrEqualTo(maxLength);
            trimmedText.Should().Be(expectedResult);
        }
    }
}
