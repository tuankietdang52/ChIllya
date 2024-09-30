using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChIllya.Models
{
    public class SongMessage
    {
        public string StatusImageSource { get; } = "";
        //play, stop, replay
        public ICommand? Command { get; }
        public Song? Current { get; }

        public SongMessage(Song current, string statusImageSource, ICommand command)
        {
            Current = current;
            StatusImageSource = statusImageSource;
            Command = command;
        }

        public (Song, string, ICommand) GetData()
        {
            return (Current, StatusImageSource, Command)!;
        }
    }
}
