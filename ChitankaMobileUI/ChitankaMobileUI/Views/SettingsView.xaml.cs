using ChitankaMobileUI.Configuration;
using ChitankaMobileUI.Helpers;
using ChitankaMobileUI.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChitankaMobileUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private async void ConnectBtn_Clicked(object sender, System.EventArgs e)
        {
            await AuthHelper.GoogleAuth();
        }

        private async void ChooseFolderButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GDriveFolderView());
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (GlobalConfig.Instance.Properties.ContainsKey("dSaveFolderName"))
            {
                FolderNameText.Text = GlobalConfig.Instance.Properties["dSaveFolderName"] as string;
            }
            if (StaticDriveAPI.Instance.IsLoggedIn())
            {
                ConnectBtn.Text = "Reconnect Google Account";
            }
        }
    }
}