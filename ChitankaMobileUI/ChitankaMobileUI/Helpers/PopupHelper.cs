using System.Threading.Tasks;

namespace ChitankaMobileUI.Helpers
{
    public static class PopupHelper
    {
        public static async Task DisplayAlert(string title, string message, string cancel)
        {
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public static async Task DisplayAlert(string title, string message, string accept, string cancel)
        {
            await App.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
