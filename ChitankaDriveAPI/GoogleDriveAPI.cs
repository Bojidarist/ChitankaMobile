using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ChitankaDriveAPI
{
    public class GoogleDriveAPI
    {
        private string AuthPage => $"https://accounts.google.com/o/oauth2/auth?access_type=offline&response_type=code&client_id={ DriveApp.ClientId }&redirect_uri={ DriveApp.RedirectUri }&scope=";
        private static UserCredential credential;
        private static DriveService service;
        private AccessTokenResponse tokenResponse;

        public GoogleDriveApp DriveApp { get; set; }
        public AccessTokenResponse TokenResponse { get => tokenResponse; set => tokenResponse = value; }

        public GoogleDriveAPI(GoogleDriveApp app, AccessTokenResponse tokenResponse = null)
        {
            DriveApp = app;
            TokenResponse = tokenResponse;
        }

        public bool IsLoggedIn()
        {
            return tokenResponse != null ? tokenResponse.AccessToken != null
                && tokenResponse.RefreshToken != null
                && tokenResponse.TokenType != null : false;
        }

        public string GetAuthURL(string scope, bool tryOpenInBrowser)
        {
            string url = AuthPage + scope;
            if (tryOpenInBrowser)
            {
                try
                {
                    Process.Start(url);
                }
                catch
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        url = url.Replace("&", "^&");
                        Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        Process.Start("xdg-open", url);
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        Process.Start("open", url);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return url;
        }

        public async Task<AccessTokenResponse> GetAuthToken(string clientCode)
        {
            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>()
                {
                    { "grant_type", "authorization_code" },
                    { "code", clientCode },
                    { "client_id", DriveApp.ClientId },
                    { "client_secret", DriveApp.ClientSecret },
                    { "redirect_uri", DriveApp.RedirectUri.ToString() }
                };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://accounts.google.com/o/oauth2/token", content);
                string responseString = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<AccessTokenResponse>(responseString);
                TokenResponse = token;
                return token;
            }
        }

        public async Task<AccessTokenResponse> RefreshAuthToken(string refreshToken)
        {
            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>()
                {
                    { "grant_type", "refresh_token" },
                    { "client_id", DriveApp.ClientId },
                    { "client_secret", DriveApp.ClientSecret },
                    { "refresh_token", refreshToken }
                };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://accounts.google.com/o/oauth2/token", content);
                string responseString = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<AccessTokenResponse>(responseString);
                token.RefreshToken = refreshToken;
                TokenResponse = token;
                return token;
            }
        }

        public void InitDriveService()
        {
            var response = new TokenResponse()
            {
                AccessToken = TokenResponse.AccessToken,
                ExpiresInSeconds = TokenResponse.ExpiresIn,
                RefreshToken = TokenResponse.RefreshToken,
                TokenType = TokenResponse.TokenType,
                Scope = TokenResponse.Scope,
                IssuedUtc = TokenResponse.IssuedUtc
            };
            var init = new GoogleAuthorizationCodeFlow.Initializer()
            {
                ClientSecrets = new ClientSecrets()
                {
                    ClientId = DriveApp.ClientId,
                    ClientSecret = DriveApp.ClientSecret
                }
            };
            var flow = new GoogleAuthorizationCodeFlow(init);
            credential = new UserCredential(flow, "user", response);

            // Create Drive API service.
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = DriveApp.ApplicationName,
            });
        }

        public void UploadFile(string path, string name, IList<string> parentId, string type = "image/jpeg")
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                Parents = parentId
            };
            using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                var request = service.Files.Create(fileMetadata, stream, type);
                request.Fields = "id";
                request.Upload();
            }
        }

        public void DownloadDriveFile(string fileId, string filePath)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile($"http://drive.google.com/uc?export=download&id={ fileId }", filePath);
            }
        }

        public async Task<FileListResponse> GetFiles(string q = "", int pageSize = 100, string orderBy = "", string pageToken = "")
        {
            string url = $"https://www.googleapis.com/drive/v3/files/?q={ q }&pageSize={ pageSize }&orderBy={ orderBy }&key={ pageToken }";

            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.Authorization] = $"{TokenResponse.TokenType} { TokenResponse.AccessToken }";
                var json = await client.DownloadStringTaskAsync(url);
                var response = JsonConvert.DeserializeObject<FileListResponse>(json);
                return response;
            }
        }
    }
}
