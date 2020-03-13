using ChitankaAPI;
using ChitankaMobileUI.Models;
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
        }

        private void CWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            // This regex checks if the url is a book and gets the id
            Match regex = new Regex(@"(\d+)[-b]*.epub").Match(e.Url);
            if (regex.Success)
            {
                string id = regex.Groups[1].Value;
                CBook book = CApi.SearchBooks(id, "id", "exact").Books[0];
                ChitankaBookModel cBook = new ChitankaBookModel() { Book = book, DownloadURL = e.Url };
                Navigation.PushModalAsync(new NavigationPage(new BookDetailView(cBook)));
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