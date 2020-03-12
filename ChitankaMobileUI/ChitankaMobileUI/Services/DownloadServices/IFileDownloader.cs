using System;

namespace ChitankaMobileUI.Services
{
    public interface IFileDownloader
    {
        void DownloadFile(string url, string folder, string fileName);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
    }
}
