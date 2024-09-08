using ChIllya.Models;
using ChIllya.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace ChIllya.ViewModels
{
    /// <summary>
    /// View Model for Song Page
    /// <para>
    /// If move to another page, current Song View Model Instance 
    /// will be deleted
    /// </para>
    /// <para>
    ///     When move to Song Page in any way, 
    ///     always create the new Song VM instance
    ///     and setup properties depend on song status through Music Player Instance 
    /// </para>
    /// </summary>
    public partial class SongViewModel : ObservableObject, IViewModel, IObserveSong
    {
        [ObservableProperty]
        private Song? current;

        private MusicManager Manager => MusicManager.Instance!;

        [ObservableProperty]
        private string? imageStatus;

        [ObservableProperty]
        private string? position;

        [ObservableProperty]
        private double sliderValue;

        private readonly System.Timers.Timer timer = new(1000);

        #region Command

        public ICommand? BackCommand { get; set; }

        [ObservableProperty]
        private ICommand? musicCommand;

        #endregion

        // User return to song page by shortcut or in directory
        public SongViewModel() {
            Setup();
            SetViewPosition();
        }

        // User go to song page by click on new song
        public SongViewModel(Song song)
        {
            Setup();
            Manager.ReadyToPlay(song);
        }

        // update information of current song
        public void Update()
        {
            Current = Manager.Current;
            ImageStatus = Manager.ImageStatus!.Source;
            MusicCommand = Manager.MusicCommand!;

            TimerControl(Manager.IsPlaying());
        }

        private void Setup()
        {
            Manager.Subscribe(this);
            SetTimer();
            GenerateCommand();
        }

        private void SetTimer()
        {
            timer.Elapsed += (source, e) => SetViewPosition();
        }

        /// <summary>
        ///  Tracks remain time of music and convert it to string
        /// </summary>
        private void SetViewPosition()
        {
            double remain = Manager.GetDuration() - Manager.GetPosition();
            Position = TimeSpan.FromSeconds((int)remain).ToString("hh\\:mm\\:ss");
            SliderValue = Manager.GetPosition();
        }

        private void TimerControl(bool isPlaying)
        {
            if (!isPlaying) timer.Stop();
            else timer.Start();

            SetViewPosition();
        }

        public void GenerateCommand()
        {
            BackCommand = new RelayCommand(async () =>
            {
                Manager.Unsubscribe(this);
                timer.Dispose();

                await Shell.Current.Navigation.PopAsync();
            });
        }

        public void SliderValueChanged(double value)
        {
            SliderValue = value;
            Manager.SeekSong(SliderValue);

            SetViewPosition();
        }
    }
}
