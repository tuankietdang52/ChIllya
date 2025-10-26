namespace ChIllya.Config
{
    public delegate void OnCompleteTaskHandler(object sender);
    public delegate void OnErrorTaskHandler(object sender, Exception ex);

    public interface IConfigure
    {
        public event OnCompleteTaskHandler? OnCompleteTask;
        public event OnErrorTaskHandler? OnErrorTask;

        public void StartConfigProgress();
    }
}