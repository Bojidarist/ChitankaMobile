using ChitankaMobileUI.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChitankaMobileUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalBooksView : ContentPage
    {
        public LocalBooksView()
        {
            InitializeComponent();
            BooksList.ItemsSource = BookKeeperService.Books;
        }

        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {

        }
    }
}