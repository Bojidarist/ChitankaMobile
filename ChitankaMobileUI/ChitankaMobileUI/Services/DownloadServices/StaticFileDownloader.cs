using System.Collections.Generic;
using Xamarin.Forms;

namespace ChitankaMobileUI.Services
{
    public static class StaticFileDownloader
    {
        public static readonly IFileDownloader Downloader = DependencyService.Get<IFileDownloader>();
        public static Dictionary<string, List<string>> DownloadPaths = new Dictionary<string, List<string>>();
    }
}
