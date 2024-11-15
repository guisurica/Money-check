using System.Text.RegularExpressions;

namespace Money.Application.Common
{
    public static class TextCleaner
    {
        public static string RemoveWhiteSpaces(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string cleanedText = Regex.Replace(input, @"\s+", "");

            return cleanedText;
        }
    }
}
