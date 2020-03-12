using ChitankaAPI;
using ChitankaMobileUI.Services;
using ChitankaMobileUI.ViewModels;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChitankaMobileUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebPageView : ContentPage
    {
        private WebPageViewModel vm;

        public WebPageView()
        {
            InitializeComponent();
            vm = new WebPageViewModel();
            BindingContext = vm;
            StaticFileDownloader.Downloader.OnFileDownloaded += Downloader_OnFileDownloaded;
        }

        private void Downloader_OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            if (e.FileSaved)
            {
                DisplayAlert("File Download", "File saved", "Close");
            }
            else
            {
                DisplayAlert("File Download", "File not saved", "Close");
            }
        }

        private void CWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            // This regex checks if the url is a book and gets the id
            Match regex = new Regex(@"-(\d+)-b.epub").Match(e.Url);
            if (regex.Success)
            {
                string id = regex.Groups[1].Value;
                CBook book = CApi.SearchBooks(id, "id", "exact").Books[0];
                StaticFileDownloader.Downloader.DownloadFile(e.Url, "ChitankaDownloads",
                    $"{ book.Id }-{ book.Title }.epub");
                // Add book to Google drive helper
                BookKeeperService.Books.Add(book);
            }
            else
            {
                vm.LastPage = e.Url;
            }
        }

        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(vm.LastPage))
            {
                CWebView.Source = vm.LastPage;
            }
        }
    }
}