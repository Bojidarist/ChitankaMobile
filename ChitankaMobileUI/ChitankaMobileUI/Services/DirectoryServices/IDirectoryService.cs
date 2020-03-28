namespace ChitankaMobileUI.Services
{
    public interface IDirectoryService
    {
        string DownloadsPath { get; set; }
        string ExternalStoragePath { get; set; }
    }
}
