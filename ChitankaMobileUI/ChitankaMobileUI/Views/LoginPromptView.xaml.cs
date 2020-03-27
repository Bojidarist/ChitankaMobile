using ChitankaMobileUI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChitankaMobileUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPromptView : ContentPage
    {
        public LoginPromptView()
        {
            InitializeComponent();
            BindingContext = new LoginPromptViewModel();
        }
    }
}