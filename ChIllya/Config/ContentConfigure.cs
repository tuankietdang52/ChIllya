using ChIllya.Views.Contents;

namespace ChIllya.Config
{
    public class ContentConfigure : IConfigure
    {
        public event OnCompleteTaskHandler? OnCompleteTask;
        public event OnErrorTaskHandler? OnErrorTask;

        public void StartConfigProgress()
        {
            InitializeContent();
            OnCompleteTask?.Invoke(this);
        }

        private void InitializeContent()
        {
            _ = new HomeView();
            _ = new DownloadView();
            _ = new DirectoryView();
        }
    }
}