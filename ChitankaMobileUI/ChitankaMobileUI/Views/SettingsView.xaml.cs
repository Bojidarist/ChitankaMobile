using ChitankaDriveAPI;
using ChitankaMobileUI.Helpers;
using ChitankaMobileUI.Services;
using System;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChitankaMobileUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private async void ConnectBtn_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                HttpListener listener = new HttpListener();
                listener.Prefixes.Add("http://127.0.0.2:4321/");
                listener.Start();
                StaticDriveAPI.Instance.GetAuthURL(GoogleDriveScopes.DriveFile, true);
                bool isLoggedIn = false;
                while (!isLoggedIn)
                {
                    HttpListenerContext context = listener.GetContext();
                    if (context != null)
                    {
                        string msg = "<h1>All done here</h1>";
                        context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(msg);
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        using (Stream stream = context.Response.OutputStream)
                        {
                            using (StreamWriter writer = new StreamWriter(stream))
                            {
                                // Return a message to the user
                                writer.Write(msg);

                                // Get user code
                                string rawUrl = context.Request.RawUrl;
                                int codeIndex = rawUrl.IndexOf('=') + 1;
                                string clientCode = rawUrl.Substring(codeIndex);
                                var token = await StaticDriveAPI.Instance.GetAuthToken(clientCode);
                                token.WriteToLocalJsonFile();

                                // Initialize Drive service
                                StaticDriveAPI.Instance.InitDriveService();
                                isLoggedIn = true;
                            }
                        }
                    }
                }
                listener.Stop();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}