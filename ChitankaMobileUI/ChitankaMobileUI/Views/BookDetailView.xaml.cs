using ChitankaMobileUI.Models;
using ChitankaMobileUI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChitankaMobileUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailView : ContentPage
    {
        public BookDetailView(ChitankaBookModel book)
        {
            InitializeComponent();
            BindingContext = new BookDetailViewModel(book);
        }
    }
}