using ChitankaAPI;
using ChitankaMobileUI.Helpers;
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
            NavigationHelper.OnBackButtonPressed += BackButtonPressed;
        }

        private void BackButtonPressed()
        {
            if (CWebView.CanGoBack)
            {
                CWebView.GoBack();
            }
        }

        private async void CWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            // This regex checks if the url is a book and gets the id
            Match regex = new Regex(@"(\d+)[-b]*.epub").Match(e.Url);
            if (regex.Success)
            {
                string id = regex.Groups[1].Value;
                CBook book = CApi.SearchBooks(id, "id", "exact").Books[0];
                ChitankaBookModel cBook = new ChitankaBookModel() { Book = book, DownloadURL = e.Url };

                await Navigation.PushModalAsync(new NavigationPage(new BookDetailView(cBook)));
            }
            else
            {
                bool regexFb = new Regex(@"(\d+)[-b]*.fb2.zip").Match(e.Url).Success;
                bool regexTxt = new Regex(@"(\d+)[-b]*.txt.zip").Match(e.Url).Success;
                bool regexSfb = new Regex(@"(\d+)[-b]*.sfb.zip").Match(e.Url).Success;
                if (!(regexFb || regexTxt || regexSfb))
                {
                    vm.LastPage = e.Url;
                }
            }
        }

        private void RefreshToHomeClicked(object sender, System.EventArgs e)
        {
            CWebView.Source = vm.MainPage;
        }

        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {
            if (vm.LastPage != null)
            {
                CWebView.Source = vm.LastPage;
            }
            else
            {
                CWebView.Source = vm.MainPage;
            }
        }
    }
}