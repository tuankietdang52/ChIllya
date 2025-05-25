using ChIllya.Utils;
using ChIllya.Views;
using ChIllya.Views.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Config
{
    public class AppSetup
    {
        private readonly ProgressReport progressReport = new();
        private readonly IProgress<ProgressReport> progress;

        private readonly List<IConfigure> configures = [];

        private bool isError = false;
        private double eachProgressPercent;
        private bool isFinish = false;

        public AppSetup()
        {
            progress = new Progress<ProgressReport>();
        }

        public Progress<ProgressReport> GetProgress()
        {
            return (Progress<ProgressReport>)progress;
        }

        public void AddConfigure(IConfigure configure)
        {
            configures.Add(configure);
        }

        private async void OnProgressChanged(object sender)
        {
            await Task.Delay(100);

            if (!isFinish) progressReport.PercentComplete += eachProgressPercent;
            else progressReport.PercentComplete = 1;

            progress.Report(progressReport);
        }

        private async void OnProgressError(object sender, Exception ex)
        {
            isError = true;

            WarningPopup.DisplayError(ex.Message);
            await Task.Delay(1500);
            App.Current?.Quit();
        }

        public async void Start()
        {
            int total = configures.Count + 1;
            eachProgressPercent = (double)1  / (double)total;

            foreach (IConfigure configure in configures)
            {
                if (isError) return;
                configure.OnCompleteTask += OnProgressChanged;
                configure.OnErrorTask += OnProgressError;

                await Task.Delay(100);
                configure.StartConfigProgress();
            }

            PrepareApp();
            isFinish = true;

            OnProgressChanged(this);
        }

        private async void PrepareApp()
        {
            await Task.Delay(100);

            _ = MainPage.Instance;
            OnProgressChanged(this);
        }
    }
}