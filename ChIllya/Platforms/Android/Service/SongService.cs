using Android.Content;
using Android.Database;
using Android.Provider;
using ChIllya.Models;
using System.Diagnostics;

namespace ChIllya.Services.Implementations
{
    public partial class SongService : ISongService
    {
#pragma warning disable CA1416
        private partial List<string> GetData()
        {
            List<string> result = [];
            string[] projection =
            {
                MediaStore.Audio.Media.InterfaceConsts.IsMusic,
                MediaStore.Audio.Media.InterfaceConsts.Data,
            };

            Context? context = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;

            ICursor? cursor = context?.ContentResolver?
                .Query(MediaStore.Audio.Media.ExternalContentUri!, projection, null, null);

            if (cursor is null) return result;
            if (!cursor.MoveToFirst())
            {
                cursor.Close();
                return result;
            }

            do
            {
                if (cursor.GetString(0) == "0") continue;

                string path = cursor.GetString(1)!;
                result.Add(path);
            } 
            while (cursor.MoveToNext());

            cursor.Close();
            return result;
        }
#pragma warning restore CA1416
    }
}