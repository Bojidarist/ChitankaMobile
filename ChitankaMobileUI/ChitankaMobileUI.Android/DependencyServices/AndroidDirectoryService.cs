using Android.OS;
using ChitankaMobileUI.Droid.DependencyServices;
using ChitankaMobileUI.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidDirectoryService))]
namespace ChitankaMobileUI.Droid.DependencyServices
{
    public class AndroidDirectoryService : IDirectoryService
    {
        public string DownloadsPath { get; set; } = Environment.DirectoryDownloads;
        public string ExternalStoragePath { get; set; } = Environment.ExternalStorageDirectory.AbsolutePath;
    }
}