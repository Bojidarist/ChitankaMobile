using Xamarin.Forms;

namespace ChitankaMobileUI.Services
{
    public static class StaticDirectoryService
    {
        public static IDirectoryService Dirs = DependencyService.Get<IDirectoryService>();
    }
}
