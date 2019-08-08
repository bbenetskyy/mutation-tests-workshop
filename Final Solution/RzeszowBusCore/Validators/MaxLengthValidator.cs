using System;
using System.Collections.Generic;
using System.Text;

namespace RzeszowBusCore.Validators
{
    public static class MaxLengthValidator
    {
        public static string TrimToMaxLength(this string text, int maxLength) =>
            string.IsNullOrWhiteSpace(text)
                ? string.Empty
                : text.Length + 1 <= maxLength
                    ? text
                    : text.Substring(0, maxLength);
    }
}
