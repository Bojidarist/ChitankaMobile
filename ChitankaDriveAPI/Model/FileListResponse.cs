using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChitankaDriveAPI
{
    public class FileListResponse
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("nextPageToken")]
        public string NextPageToken { get; set; }

        [JsonProperty("incompleteSearch")]
        public bool IncompleteSearch { get; set; }

        [JsonProperty("files")]
        public List<DriveFile> Files { get; set; }
    }
}
