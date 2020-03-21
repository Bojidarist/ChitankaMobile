using System.Collections.Generic;
using Xamarin.Forms;

namespace ChitankaMobileUI.Services
{
    public static class StaticFileDownloader
    {
        public static readonly IFileDownloader Downloader = DependencyService.Get<IFileDownloader>();
        public static HashSet<string> DownloadPaths = new HashSet<string>();
    }
}
