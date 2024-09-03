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
using System.Windows.Input;

namespace ChIllya.ViewModels
{
    /// <summary>
    /// View Model for Song Page
    /// <para>
    /// If move to another page, current Song View Model Instance 
    /// will be deleted even event
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

        #region Command

        public ICommand? BackCommand { get; set; }

        [ObservableProperty]
        private ICommand? musicCommand;

        #endregion

        // User return to song page by shortcut or in directory
        public SongViewModel() {
            Setup();
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
        }

        private void Setup()
        {
            Manager.Subscribe(this);
            GenerateCommand();
        }

        public void GenerateCommand()
        {
            BackCommand = new RelayCommand(async () =>
            {
                Manager.Unsubscribe(this);
                await Shell.Current.Navigation.PopAsync();
            });
        }
    }
}
