using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace PME
{
    [DebuggerStepThrough]
    public static class StringExtensions
    {
        
        public static bool EqualsIgnoreCase(this string str, string compareTo)
        {
            if (string.IsNullOrEmpty(str))
            {
                return (string.IsNullOrEmpty(compareTo));
            }

            return str.Equals(compareTo, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsPresent(this string value)
        {
            return !string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value);
        }

        public static bool IsMissing(this string value)
        {
            return string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value);
        }

        public static bool IsValidGuid(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            var newGuid = Guid.Empty;

            try
            {
                newGuid = new Guid(input);
            }
            catch (FormatException)
            {
                // Ignore
            }

            return (newGuid != Guid.Empty);
        }

        public static bool IsBase64(this string base64String)
        {
            // Credit: oybek http://stackoverflow.com/users/794764/oybek
            if (IsMissing(base64String)
                || base64String.Length % 4 != 0
                || base64String.Contains(" ")
                || base64String.Contains("\t")
                || base64String.Contains("\r")
                || base64String.Contains("\n")
                )
            {
                return false;
            }

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception)
            {
                // gulp
            }

            return false;
        }

        [DebuggerStepThrough]
        public static string SafeTrim(this string input, params char[] trimChars)
        {
            if (input.IsPresent())
            {
                return input.Trim(trimChars);
            }

            return string.Empty;
        }

        public static string WithQuotes(this string input)
        {
            return string.Concat(
                "\"",
                input,
                "\""
                );
        }
        
        
    }
}

