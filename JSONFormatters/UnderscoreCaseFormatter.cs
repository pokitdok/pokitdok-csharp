namespace pokitdokcsharp.JSONFormatters
{
    /// <summary>
    /// Converts a string to underscore_case
    /// </summary>
    public static class UnderscoreCaseFormatter
    {
        /// <summary>
        /// converts string to lower_case format
        /// </summary>
        /// <param name="strToConvert"></param>
        /// <returns></returns>
        public static string ToUnderscoreCase(this string strToConvert)
            =>
                System.Text.RegularExpressions.Regex.Replace(strToConvert, @"([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])",
                    "$1$3_$2$4").ToLower();
    }
}