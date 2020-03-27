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
        }

        private void FilesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DriveFile file = e.SelectedItem as DriveFile;
            GlobalConfig.Instance.Properties["dSaveFolderId"] = file.Id;
            GlobalConfig.Instance.Properties["dSaveFolderName"] = file.Name;
            GlobalConfig.Instance.Save();
            Navigation.PopAsync();
        }
    }
}