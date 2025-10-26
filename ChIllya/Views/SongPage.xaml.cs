using ChIllya.ViewModels;

namespace ChIllya.Views
{
    public partial class SongPage : ContentPage
    {
        public SongViewModel ViewModel { get; set; }

        public SongPage()
        {
            InitializeComponent();
            ViewModel = new SongViewModel();
            BindingContext = ViewModel;
        }

        private void SliderValueChanged(object sender, EventArgs e)
        {
            ViewModel.SliderValueChanged(this.MusicSlider.Value);           
        }
    }
}