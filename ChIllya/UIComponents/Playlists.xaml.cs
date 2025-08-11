using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChIllya.Models;

namespace ChIllya.UIComponents
{
    public partial class Playlists : ContentView
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<Playlist>), typeof(Playlists));

        public IEnumerable<Playlist> ItemsSource
        {
            get => (IEnumerable<Playlist>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public Playlists()
        {   
            InitializeComponent();
        }
    }
}