using ChIllya.Utils;
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

        private void OnProgressChanged(object sender)
        {
            progressReport.PercentComplete += eachProgressPercent;
            progress.Report(progressReport);
        }

        private async void OnProgressError(object sender, Exception ex)
        {
            isError = true;

            WarningPopup.DisplayError(ex.Message);
            await Task.Delay(1500);
            App.Current?.Quit();
        }

        public void Start()
        {
            int total = configures.Count;
            eachProgressPercent = (1 * 100) / total;

            foreach (IConfigure configure in configures)
            {
                if (isError) return;
                configure.OnCompleteTask += OnProgressChanged;
                configure.OnErrorTask += OnProgressError;

                configure.StartConfigProgress();
            }
        }
    }
}