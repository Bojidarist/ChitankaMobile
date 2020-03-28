using ChitankaDriveAPI;
using ChitankaMobileUI.Configuration;
using ChitankaMobileUI.ViewModels;
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
            BindingContext = new GDriveFolderViewModel(files);
            Helpers.NavigationHelper.OnBackButtonPressed += BackButtonPressed;
        }

        private async void BackButtonPressed()
        {
            await Navigation.PopAsync();
        }

        private void FilesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DriveFile file = e.SelectedItem as DriveFile;
            GlobalConfig.Instance.Properties[GlobalConfig.Options.DSaveFolderId] = file.Id;
            GlobalConfig.Instance.Properties[GlobalConfig.Options.DSaveFolderName] = file.Name;
            GlobalConfig.Instance.Save();
            Navigation.PopAsync();
        }
    }
}