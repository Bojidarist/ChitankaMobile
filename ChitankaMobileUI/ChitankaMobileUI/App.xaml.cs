using ChitankaDriveAPI;
using ChitankaMobileUI.Helpers;
using ChitankaMobileUI.Services;
using ChitankaMobileUI.Views;
using Newtonsoft.Json;
using System;
using System.IO;
using Xamarin.Forms;

namespace ChitankaMobileUI
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            // Delete files in downloads directory
            try
            {
                if (Directory.Exists(DirHelper.ChitankaDownloadsPath))
                {
                    foreach (var file in Directory.GetFiles(DirHelper.ChitankaDownloadsPath))
                    {
                        File.Delete(file);
                    }
                }
            }
            catch (Exception)
            {
            }

            // Refresh token on startup
            try
            {
                string tokenPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "token.json");
                if (File.Exists(tokenPath))
                {
                    string tokenResponseText = File.ReadAllText(tokenPath);
                    var currentToken = JsonConvert.DeserializeObject<AccessTokenResponse>(tokenResponseText);
                    var newToken = await StaticDriveAPI.Instance.RefreshAuthToken(currentToken.RefreshToken);
                    newToken.WriteToLocalJsonFile();
                    StaticDriveAPI.Instance.InitDriveService();
                }
            }
            // Don't do this
            catch (Exception)
            {
                await MainPage.Navigation.PushAsync(new LoginPromptView());
            }

            if (!StaticDriveAPI.Instance.IsLoggedIn())
            {
                await MainPage.Navigation.PushAsync(new LoginPromptView());
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
