using ChitankaDriveAPI;
using ChitankaMobileUI.Helpers;
using ChitankaMobileUI.Services;
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
            // Refresh token on startup
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

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
