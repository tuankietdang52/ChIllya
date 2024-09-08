using ChIllya.Models;
using ChIllya.ViewModels;

namespace ChIllya.Views
{
    public partial class SongPage : ContentPage
    {
        public SongViewModel ViewModel { get; set; }

        public SongPage(Song? current = null)
        {
            InitializeComponent();

            if (current == null) ViewModel = new();
            else ViewModel = new(current);

            BindingContext = ViewModel;
        }

        private void SliderValueChanged(object sender, EventArgs e)
        {
            ViewModel.SliderValueChanged(this.MusicSlider.Value);           
        }
    }
}