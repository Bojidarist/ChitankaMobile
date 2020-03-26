using ChitankaDriveAPI;
using ChitankaMobileUI.Services;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ChitankaMobileUI.Helpers
{
    public static class AuthHelper
    {
        public static async Task GoogleAuth()
        {
            try
            {
                HttpListener listener = new HttpListener();
                listener.Prefixes.Add("http://127.0.0.2:4321/");
                listener.Start();
                string authUrl = StaticDriveAPI.Instance.GetAuthURL(GoogleDriveScopes.Drive, false);
                await Browser.OpenAsync(authUrl);
                bool isLoggedIn = false;
                while (!isLoggedIn)
                {
                    HttpListenerContext context = listener.GetContext();
                    if (context != null)
                    {
                        // Get user code
                        string rawUrl = context.Request.RawUrl;
                        int codeIndex = rawUrl.IndexOf('=') + 1;
                        string clientCode = rawUrl.Substring(codeIndex);
                        var token = await StaticDriveAPI.Instance.GetAuthToken(clientCode);
                        token.WriteToLocalJsonFile();

                        // Initialize Drive service
                        StaticDriveAPI.Instance.InitDriveService();

                        isLoggedIn = true;
                        string msg = "<h1>All done here</h1>";
                        context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(msg);
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        using (Stream stream = context.Response.OutputStream)
                        {
                            using (StreamWriter writer = new StreamWriter(stream))
                            {
                                // Return a message to the user
                                writer.Write(msg);
                            }
                        }
                    }
                }
                listener.Stop();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
