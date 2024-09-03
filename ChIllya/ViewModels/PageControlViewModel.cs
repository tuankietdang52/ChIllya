using ChIllya.Models;
using ChIllya.Utils;
using ChIllya.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChIllya.ViewModels
{
    public partial class PageControlViewModel : ObservableObject, IObserveSong
    {
        private MusicManager Manager => MusicManager.Instance!;

        [ObservableProperty]
        private Song? current;

        [ObservableProperty]
        private string? imageStatus;

        #region Command

        public ICommand? ReturnCommand { get; set; }

        [ObservableProperty]
        private ICommand? musicCommand;

        #endregion

        public PageControlViewModel()
        {
            Manager.Subscribe(this);
            ReturnCommand = new RelayCommand(ReturnToSongPage);
        }

        public void Update()
        {
            Current = Manager.Current;
            ImageStatus = Manager.ImageStatus!.Source;
            MusicCommand = Manager.MusicCommand;
        }

        private async void ReturnToSongPage()
        {
            await Shell.Current.Navigation.PushAsync(new SongPage());
        }
    }
}
