using ChIllya.Views.Contents;

namespace ChIllya.Config
{
    public class ContentConfigure : IConfigure
    {
        private static bool isLoaded = false;

        public event OnCompleteTaskHandler? OnCompleteTask;
        public event OnErrorTaskHandler? OnErrorTask;

        public void StartConfigProgress()
        {
            InitializeContent();
            OnCompleteTask?.Invoke(this);
            isLoaded = true;
        }

        private void InitializeContent()
        {
            if (isLoaded) return;
            
            _ = new HomeView();
            _ = new DownloadView();
            _ = new DirectoryView();
        }
    }
}