using System;

namespace ChitankaMobileUI.Services
{
    public class DownloadEventArgs : EventArgs
    {
        public bool FileSaved = false;
        public string Path;

        public DownloadEventArgs(bool fileSaved, string path)
        {
            FileSaved = fileSaved;
            Path = path;
        }
    }
}
