using ChitankaDriveAPI;
using Newtonsoft.Json;
using System;
using System.IO;

namespace ChitankaMobileUI.Helpers
{
    public static class AccessTokenResponseHelpers
    {
        public static void WriteToLocalJsonFile(this AccessTokenResponse tokenResponse)
        {
            string json = JsonConvert.SerializeObject(tokenResponse);
            string jsonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "token.json");
            File.WriteAllText(jsonPath, json);
        }
    }
}
