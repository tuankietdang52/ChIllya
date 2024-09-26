using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Utils
{
    public static class EnvLoader
    {
        public static void Load(string filePath)
        {
            StreamReader sr;
            try
            {
                var task = FileSystem.Current.OpenAppPackageFileAsync(filePath);
                task.Wait();

                Stream fileStream = task.Result;
                sr = new StreamReader(fileStream);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            string? line;

            while ((line = sr.ReadLine()) != null){
                // Skip empty lines and comments
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                {
                    continue;
                }

                // Skip lines that are not key-value pairs
                var parts = line.Split('=', 2);
                if (parts.Length != 2) continue;

                var key = parts[0].Trim();
                var value = parts[1].Trim();
                Environment.SetEnvironmentVariable(key, value);
            }
        }
    }
}
