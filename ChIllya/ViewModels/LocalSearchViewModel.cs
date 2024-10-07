using ChIllya.Models;
using ChIllya.Services;
using ChIllya.Services.Implementations;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChIllya.ViewModels
{
    public class LocalSearchViewModel : IViewModel
    {
        private readonly ILocalService _localService;
        private readonly ObservableCollection<Song> songs;

        public ICommand? SearchCommand {  get; set; }

        public LocalSearchViewModel(ILocalService localService)
        {
            _localService = localService;
            songs = new();

            GenerateCommand();
        }

        public void GenerateCommand()
        {
            SearchCommand = new RelayCommand<string>(Searching!);
        }

        public void Searching(string query)
        {
            
        }
    }
}
