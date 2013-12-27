using System;

namespace DR.Async.Task.Helpers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Apply format to string.
        /// </summary>
        /// <param name="str">String format.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>String with applied format.</returns>
        public static string FormatString(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static string Fmt(this string str, params object[] args)
        {
            return FormatString(str, args);
        }

        /// <summary>
        /// Apply format to string.
        /// </summary>
        /// <param name="str">String format.</param>
        /// <param name="formatProfider">The culture.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>
        /// String with applied format.
        /// </returns>
        public static string FormatUIString(
            this string str, IFormatProvider formatProfider, params object[] args)
        {
            return string.Format(formatProfider, str, args);
        }

        public static string Fmt(
            this string str, IFormatProvider formatProfider, params object[] args)
        {
            return FormatUIString(str, formatProfider, str, args);
        }
    }
}
