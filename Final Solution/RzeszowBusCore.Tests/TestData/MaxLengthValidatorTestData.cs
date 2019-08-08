using System;
using System.Collections.Generic;
using System.Text;

namespace RzeszowBusCore.Tests.TestData
{
    public static class MaxLengthValidatorTestData
    {
        public static IEnumerable<object[]> TextWithMaxLenghAndExpectedResult => new List<object[]>
        {
            new object[] { new string('*', 3), 2, new string('*', 2) },
            new object[] { new string('*', 3), 3, new string('*', 3) },
            new object[] { new string('*', 3), 4, new string('*', 3) },
            new object[] { "        ", 4, string.Empty },
            new object[] { string.Empty, 4, string.Empty },
            new object[] { null, 4, string.Empty },
        };
    }
}
