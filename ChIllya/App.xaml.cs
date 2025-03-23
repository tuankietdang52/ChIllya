
using ChIllya.Config;
using ChIllya.Models;
using ChIllya.Services;
using ChIllya.Utils;
using ChIllya.Views;
using ChIllya.Views.Popups;
using Syncfusion.Licensing;
using System.Threading.Tasks;

namespace ChIllya
{
    public partial class App : Application
    {
        public IServiceProvider? ServiceProvider { get; private set; }

        public App(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            InitializeComponent();
		}

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new SplashScreenPage());
        }
    }
}
