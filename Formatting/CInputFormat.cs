using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace CorazonHeart
{
    /// <summary>
    /// Provides input formatting services.
    /// </summary>
    public class CInputFormat
    {
        /// <summary>
        /// Capitalize a string.
        /// </summary>
        /// <param name="input">The input string to capitalize.</param>
        /// <returns>Returns the capitalized version of the string.</returns>
        public string Capitalize(string input)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(input);
        }

        /// <summary>
        /// Lower case a string.
        /// </summary>
        /// <param name="input">The input string to lower case.</param>
        /// <returns>Returns the lower case version of the string.</returns>
        public string LowerCase(string input)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToLower(input);
        }

        /// <summary>
        /// Upper case a string.
        /// </summary>
        /// <param name="input">The input string to upper case.</param>
        /// <returns>Returns the upper case version of the string.</returns>
        public string UpperCase(string input)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToUpper(input);
        }

        /// <summary>
        /// Validate whether the password meets the required standards.
        /// </summary>
        /// <param name="password">The plain password to validate.</param>
        /// <returns>Returns true if password is valid, false otherwise.</returns>
        public bool ValidatePassword(string password)
        {
            // init isSuccess
            bool isValid = false;

            if (!(
                // check for null / empty string
                String.IsNullOrWhiteSpace(password)

                // Password standards:
                // 1. At least one lower case letter              (?=.*[a-z])
                // 2. At least one upper case letter              (?=.*[A-Z])
                // 2. At least one number                         (?=.*\\d)
                // 4. Length : 8 - 30 characters                  {8,30}
                || !Regex.IsMatch(password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,30}$")
                ))
            {
                // valid password
                isValid = true;
            }

            return isValid;
        }
    }
}