using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Views.Popups
{
    public static class PopUp
    {
        private static Page MainPage => App.Current!.Windows[0]!.Page!;

        /// <summary>
        /// run without await
        /// </summary>
        public static async void DisplayError(string message)
        {
            Debug.WriteLine(message);
            await MainPage.DisplayAlert("Error", message, "Close");
        }

        /// <summary>
        /// run with await
        /// </summary>>
        public static Task DisplayErrorTask(string message)
        {
            Debug.WriteLine(message);
            return MainPage.DisplayAlert("Error", message, "Close");
        }
    }
}
