using ChitankaMobileUI.Services;
using System.IO;

namespace ChitankaMobileUI.Helpers
{
    public static class DirHelper
    {
        public static string ChitankaDownloadsPath
        {
            get
            {
                return Path.Combine(StaticDirectoryService.Dirs.ExternalStoragePath, "ChitankaDownloads");
            }
        }
    }
}
