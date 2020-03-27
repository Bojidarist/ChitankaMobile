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
        private string filePath;

        public void DownloadFile(string url, string folder, string fileName)
        {
            try
            {
                string newFolderPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, folder);
                Directory.CreateDirectory(newFolderPath);

                using (WebClient client = new WebClient())
                {
                    client.DownloadFileCompleted += FileDownloadCompleted;
                    filePath = Path.Combine(newFolderPath, fileName);
                    if (!File.Exists(filePath))
                    {
                        client.DownloadFileAsync(new Uri(url), filePath);
                    }
                    else
                    {
                        OnFileDownloaded?.Invoke(this, new DownloadEventArgs(true, filePath));
                    }
                }
            }
            catch (Exception)
            {
                OnFileDownloaded?.Invoke(this, new DownloadEventArgs(false, null));
            }
        }

        private void FileDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            bool isDownloaded = e.Error == null;
            OnFileDownloaded?.Invoke(this, new DownloadEventArgs(isDownloaded, filePath));
        }
    }
}