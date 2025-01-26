using ChIllya.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Models
{
    public partial class StatusImage : ObservableObject
    {
        private MusicManager manager;

        public string? Source { get; set; } = "";

        public StatusImage(MusicManager manager)
        {
            this.manager = manager;
        }

        public void UpdateStatus()
        {
            //idk why the information of music status is loading so slow
            Thread.Sleep(200);

            if (manager.IsEnd())
            {
                Source = "Images/replay.svg";
                return;
            }

            Source = manager.IsPlaying() ? "Images/pause.svg" : "Images/play.svg";
        }
    }
}
