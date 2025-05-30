﻿using ChIllya.Models;
using ChIllya.Music;
using ChIllya.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace ChIllya.ViewModels
{
    public partial class BaseContentViewModel : ObservableObject, IRecipient<SongMessage>
    {
        private MusicManager Manager => MusicManager.Instance!;

        [ObservableProperty]
        private Song? current;

        [ObservableProperty]
        private string? imageStatus;

        [ObservableProperty]
        private bool isHaveCurrentSong = false;

        #region Command

        public ICommand? ReturnCommand { get; set; }

        [ObservableProperty]
        private ICommand? musicCommand;

        #endregion

        public BaseContentViewModel()
        {
            Manager.Register(this);
            ReturnCommand = new RelayCommand(ReturnToSongPage);
        }

        public void Receive(SongMessage message)
        {
            (Current, ImageStatus, MusicCommand) = message.GetData();

            if (Current is not null && !IsHaveCurrentSong) IsHaveCurrentSong = true;
        }

        private async void ReturnToSongPage()
        {
            await App.Instance!.PushAsync(new SongPage());
        }
    }
}