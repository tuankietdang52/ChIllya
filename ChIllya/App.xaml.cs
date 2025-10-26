using ChIllya.Views;

namespace ChIllya
{
    public partial class App : Application
    {
        public static App? Instance { get; private set; }
        public IServiceProvider? ServiceProvider { get; private set; }

        public App(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Instance = this;
            InitializeComponent();
		}

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new SplashScreenPage());
        }

        public async Task PushAsync(Page page)
        {
            await Windows[0].Navigation.PushModalAsync(page);
        }

        public async Task PopAsync()
        {
            await Windows[0].Navigation.PopModalAsync();
        }

        public Page? GetMainPage()
        {
            return Windows[0].Page;
        }
    }
}
