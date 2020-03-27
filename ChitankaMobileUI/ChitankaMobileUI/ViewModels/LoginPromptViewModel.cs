using ChitankaMobileUI.Helpers;
using ChitankaMobileUI.ViewModels.Base;
using Xamarin.Forms;

namespace ChitankaMobileUI.ViewModels
{
    public class LoginPromptViewModel : BaseViewModel
    {
        public Command GAuthCommand { get; set; }

        public LoginPromptViewModel()
        {
            InitCommands();
        }

        public void InitCommands()
        {
            GAuthCommand = new Command(async () => { await AuthHelper.GoogleAuth(); });
        }
    }
}
