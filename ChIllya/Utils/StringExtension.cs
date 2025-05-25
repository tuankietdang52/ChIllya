using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChIllya.Utils
{
    public static class StringExtension
    {
        private readonly static string invalidFileNameCharacter = @"[\\/:*?""<>|]";

        public static string SanitizeFileName(this string fileName, string replace)
        {
            return Regex.Replace(fileName, invalidFileNameCharacter, replace);
        }
    }
}
