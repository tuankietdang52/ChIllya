using CommunityToolkit.Mvvm.ComponentModel;
using ChIllya.Music;
using CommunityToolkit.Mvvm.Input;

namespace ChIllya.Models
{
    public partial class StatusImage : ObservableObject
    {
        private readonly MusicManager manager;

        public string? Source { get; set; } = "";

        public StatusImage(MusicManager manager)
        {
            this.manager = manager;
        }

        public void UpdateStatus()
        {
            Thread.Sleep(200);

            switch (manager.SongState)
            {
                case EMusicState.Playing:
                    Source = "Images/pause.svg";
                    break;

                case EMusicState.Stop or EMusicState.ReadyToChange:
                    Source = "Images/play.svg";
                    break;

                case EMusicState.Ending:
                    Source = "Images/replay.svg";
                    break;
            }
        }
    }
}