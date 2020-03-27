using ChitankaMobileUI.Enums;
using ChitankaMobileUI.Models;
using ChitankaMobileUI.Views;
using System.Threading.Tasks;

namespace ChitankaMobileUI.Helpers
{
    public static class NavigationHelper
    {
        public static async Task Navigate(ViewsEnum view, object param = null)
        {
            switch (view)
            {
                case ViewsEnum.BookDetailView:
                    await App.Current.MainPage.Navigation.PushAsync(new BookDetailView(param as ChitankaBookModel));
                    break;
                case ViewsEnum.GDriveFolderView:
                    await App.Current.MainPage.Navigation.PushAsync(new GDriveFolderView());
                    break;
                case ViewsEnum.LoginPromptView:
                    await App.Current.MainPage.Navigation.PushAsync(new LoginPromptView());
                    break;
                case ViewsEnum.SettingsView:
                    await App.Current.MainPage.Navigation.PushAsync(new SettingsView());
                    break;
                case ViewsEnum.WebPageView:
                    await App.Current.MainPage.Navigation.PushAsync(new WebPageView());
                    break;
                default:
                    break;
            }
        }
    }
}
