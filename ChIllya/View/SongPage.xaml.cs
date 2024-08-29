using ChIllya.Model;
using ChIllya.ViewModel;

namespace ChIllya.View
{
    public partial class SongPage : ContentPage
    {
        public SongPage()
        {
            InitializeComponent();
        }

        public SongPage(Song current)
        {
            InitializeComponent();
            ViewModel.StartSong(current);
        }
    }
}