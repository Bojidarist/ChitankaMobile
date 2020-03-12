using Xamarin.Forms;

namespace ChitankaMobileUI.Services
{
    public static class StaticFileDownloader
    {
        public static readonly IFileDownloader Downloader = DependencyService.Get<IFileDownloader>();
    }
}
