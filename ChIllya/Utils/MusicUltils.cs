using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;

namespace ChIllya.Utils
{
    public class MusicUltils
    {
        private readonly string windowsPath = "C:/Users/ADMIN/ChIllyaData/";

        public MusicUltils() { }

        // using later
        public void DownloadMusic()
        {
            string source = windowsPath;

            if (!Directory.Exists(source))
            {
                Directory.CreateDirectory(source);
            }

            YouTube ytb = YouTube.Default;
            YouTubeVideo vid = ytb.GetVideo("https://www.youtube.com/watch?v=1iJpA0s7jqk");

            string file = source + vid.FullName;

            File.WriteAllBytes(file, vid.GetBytes());
            ConvertToMp3(file);
        }

        private void ConvertToMp3(string fileName)
        {
            int length = fileName.Length - 4;

            // i give up =))
            string mp3Name = fileName.Substring(0, length);
            File.Move(fileName, $"{mp3Name}.mp3");
        }
    }
}
