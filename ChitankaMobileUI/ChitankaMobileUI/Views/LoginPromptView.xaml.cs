
using ChitankaMobileUI.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChitankaMobileUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPromptView : ContentPage
    {
        public LoginPromptView()
        {
            InitializeComponent();
        }

        private async void GoogleAuthButton_Clicked(object sender, System.EventArgs e)
        {
            await AuthHelper.GoogleAuth();
        }
    }
}