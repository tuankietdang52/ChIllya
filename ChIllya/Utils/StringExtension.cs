using System.Text.RegularExpressions;

namespace ChIllya.Utils
{
    public static class StringExtension
    {
        private readonly static string invalidFileNameCharacter = @"[\\/:*?""<>|]";

        public static bool IsEmpty(this string str)
        {
            return str.Length == 0;
        }

        public static string SanitizeFileNameWith(this string fileName, string replace)
        {
            return Regex.Replace(fileName, invalidFileNameCharacter, replace);
        }

        public static string ConvertToHexadecimalASCII(this char c)
        {
            return $"\\x{((int)c):X2}";
        }
    }
}
