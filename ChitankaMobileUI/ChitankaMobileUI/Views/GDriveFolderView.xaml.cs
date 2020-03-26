using ChitankaDriveAPI;
using ChitankaMobileUI.Configuration;
using ChitankaMobileUI.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChitankaMobileUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GDriveFolderView : ContentPage
    {
        public GDriveFolderView(FileListResponse files = null)
        {
            InitializeComponent();

            if (files == null)
            {
                Init();
            }
            else
            {
                FilesList.ItemsSource = files.Files;
            }
        }

        private void FilesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DriveFile file = e.SelectedItem as DriveFile;
            GlobalConfig.Instance.Properties["dSaveFolderId"] = file.Id;
            GlobalConfig.Instance.Properties["dSaveFolderName"] = file.Name;
            GlobalConfig.Instance.Save();
            Navigation.PopAsync();
        }

        private async Task RefreshList()
        {
            GlobalConfig.Instance.Properties["dFoldersList"] =
                    await StaticDriveAPI.Instance.GetFiles("mimeType = 'application/vnd.google-apps.folder'", 100, "viewedByMeTime desc");
            GlobalConfig.Instance.Save();
            var driveFiles = GlobalConfig.Instance.Properties["dFoldersList"] as FileListResponse;
            FilesList.ItemsSource = driveFiles.Files;
        }

        private async void Init()
        {
            if (!GlobalConfig.Instance.Properties.ContainsKey("dFoldersList")
                || string.IsNullOrWhiteSpace(GlobalConfig.Instance.Properties["dFoldersList"].ToString())
                || GlobalConfig.Instance.Properties["dFoldersList"].GetType() != typeof(FileListResponse))
            {
                await RefreshList();
            }
            else
            {
                var driveFiles = GlobalConfig.Instance.Properties["dFoldersList"] as FileListResponse;
                FilesList.ItemsSource = driveFiles.Files;
            }
        }

        private async void RefreshButton_Clicked(object sender, System.EventArgs e)
        {
            await RefreshList();
        }
    }
}