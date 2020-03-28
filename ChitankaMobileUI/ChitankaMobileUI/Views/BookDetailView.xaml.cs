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

        private void DescriptionScroll_Scrolled(object sender, ScrolledEventArgs e)
        {
            ScrollView scrollView = sender as ScrollView;
            double scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;
            double errorPercent = 0.8;

            // Touched bottom
            if (scrollingSpace * errorPercent <= e.ScrollY)
            {
                DescriptionGradient.FadeTo(0, 250, Easing.Linear);
            }
            else
            {
                DescriptionGradient.FadeTo(1, 250, Easing.Linear);
            }
        }
    }
}