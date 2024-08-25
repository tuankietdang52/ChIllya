using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Error
{
    public class InvalidSongPathException : Exception
    {
        private readonly static string s_message = "Path to the song is invalid";

        public InvalidSongPathException() : base(s_message)
        {

        }

        public override string ToString()
        {
            return s_message;
        }
    }
}
