using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Utils
{
    public partial class StatusImage : ObservableObject
    {
        private MusicManager manager;

        [ObservableProperty]
        private string source = "";

        [ObservableProperty]
        private Microsoft.Maui.Thickness scale = new(30, 0, 30, 0);

        public StatusImage(MusicManager manager)
        {
            this.manager = manager;
        }

        public void UpdateStatus()
        {
            //idk why the information of music status is loading so slow
            Thread.Sleep(150);

            if (manager.IsEnd())
            {
                Source = "Images/replay.png";
                return;
            }

            Source = manager.IsPlaying() ? "Images/pause.png" : "Images/play.png";
        }
    }
}
