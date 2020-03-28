using ChitankaMobileUI.Helpers;

namespace ChitankaMobileUI
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            NavigationHelper.InvokeOnBackButtonPressed();
            return true;
        }
    }
}
