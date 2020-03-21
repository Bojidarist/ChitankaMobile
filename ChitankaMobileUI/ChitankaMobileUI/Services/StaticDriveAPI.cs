using ChitankaDriveAPI;

namespace ChitankaMobileUI.Services
{
    public class StaticDriveAPI
    {
        private static GoogleDriveAPI api;

        public static GoogleDriveAPI Instance
        {
            get
            {
                if (api == null)
                {
                    api = new GoogleDriveAPI(new GoogleDriveApp()
                    {
                        ApplicationName = "ChitankaMobile",
                        ClientId = "ClientId",
                        ClientSecret = "ClientSecret",
                        RedirectUri = new System.Uri("http://127.0.0.2:4321/")
                    });
                }
                return api;
            }
        }
    }
}
