using ChitankaMobileUI.Droid.DependencyServices;
using ChitankaMobileUI.Services;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidFileDownloader))]
namespace ChitankaMobileUI.Droid.DependencyServices
{
    public class AndroidFileDownloader : IFileDownloader
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        public void DownloadFile(string url, string folder, string fileName)
        {
            string newFolderPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, folder);
            Directory.CreateDirectory(newFolderPath);

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFileCompleted += FileDownloadCompleted;
                    string filePath = Path.Combine(newFolderPath, fileName);
                    client.DownloadFileAsync(new Uri(url), filePath);
                }
            }
            catch (Exception)
            {
                OnFileDownloaded?.Invoke(this, new DownloadEventArgs(false));
            }
        }

        private void FileDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            bool isDownloaded = e.Error == null;
            OnFileDownloaded?.Invoke(this, new DownloadEventArgs(isDownloaded));
        }
    }
}