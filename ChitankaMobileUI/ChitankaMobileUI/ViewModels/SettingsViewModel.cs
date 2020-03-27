using ChitankaMobileUI.Configuration;
using ChitankaMobileUI.Enums;
using ChitankaMobileUI.Helpers;
using ChitankaMobileUI.Services;
using ChitankaMobileUI.ViewModels.Base;
using Xamarin.Forms;

namespace ChitankaMobileUI.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public string ConnectToGoogleText
        {
            get
            {
                return StaticDriveAPI.Instance.IsLoggedIn() ? "Reconnect Google Account" : "Connect Google Account";
            }
        }

        public string GDriveFolderName
        {
            get
            {
                if (GlobalConfig.Instance.Properties.ContainsKey(GlobalConfig.Options.DSaveFolderName))
                {
                    return GlobalConfig.Instance.Properties[GlobalConfig.Options.DSaveFolderName] as string;
                }
                return null;
            }
        }

        public Command GConnectCommand { get; set; }
        public Command GFolderSelectCommand { get; set; }

        public SettingsViewModel()
        {
            InitCommands();
            GlobalConfig.Instance.OnConfigSaved += RefreshProperties;
        }

        public void RefreshProperties()
        {
            OnPropertyChanged(nameof(ConnectToGoogleText));
            OnPropertyChanged(nameof(GDriveFolderName));
        }

        public void InitCommands()
        {
            GConnectCommand = new Command(async () => { await AuthHelper.GoogleAuth(); });
            GFolderSelectCommand = new Command(async () =>
            {
                await NavigationHelper.Navigate(ViewsEnum.GDriveFolderView);
            });
        }
    }
}
