using ChIllya.Music;
using ChIllya.Utils;

namespace ChIllya.Config
{
    public class AppConfigure : IConfigure
    {
        public event OnCompleteTaskHandler? OnCompleteTask;
        public event OnErrorTaskHandler? OnErrorTask;

        public AppConfigure() { }

        public void StartConfigProgress()
        {
            InitializeInstances();
            LoadSecretFile();

            OnCompleteTask?.Invoke(this);
        }
        
        /// <summary>
        /// Initialize all Singleton Instance
        /// </summary>
        private void InitializeInstances()
        {
            _ = MusicManager.Instance;
            _ = MusicStorage.Instance;
        }

        private void LoadSecretFile()
        {
            try
            {
                EnvLoader.Load("Secret/secret.env");
            }
            catch (Exception ex)
            {
                OnErrorTask?.Invoke(this, ex);
            }
        }
    }
}
