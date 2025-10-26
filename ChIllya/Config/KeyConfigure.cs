using Syncfusion.Licensing;

namespace ChIllya.Config
{
    public class KeyConfigure : IConfigure
    {
        public event OnCompleteTaskHandler? OnCompleteTask;
        public event OnErrorTaskHandler? OnErrorTask;

        public void StartConfigProgress()
        {
            try
            {
                RegisterSyncfusionKey();
                OnCompleteTask?.Invoke(this);
            }
            catch (Exception ex)
            {
                OnErrorTask?.Invoke(this, ex);
            }
        }

        private void RegisterSyncfusionKey()
        {
            string key = Environment.GetEnvironmentVariable("SYNCFUSION_LICENSE_KEY")!;

            if (key == null)
            {
                string errMessage = "Cant get Syncfusion key";
                OnErrorTask?.Invoke(this, new NullReferenceException(errMessage));

                return;
            }

            SyncfusionLicenseProvider.RegisterLicense(key);
        }
    }
}
