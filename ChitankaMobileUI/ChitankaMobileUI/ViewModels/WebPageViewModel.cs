using ChitankaMobileUI.ViewModels.Base;

namespace ChitankaMobileUI.ViewModels
{
    public class WebPageViewModel : BaseViewModel
    {
        private string _mainPage = "https://chitanka.info";
        private string _lastPage;

        public string LastPage
        {
            get { return _lastPage; }
            set { _lastPage = value; }
        }

        public string MainPage
        {
            get { return _mainPage; }
            set
            {
                _mainPage = value;
                OnPropertyChanged();
            }
        }

        public WebPageViewModel()
        {
            Title = "WebPage";
        }

    }
}
