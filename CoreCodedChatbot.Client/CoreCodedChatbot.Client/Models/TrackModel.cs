using Newtonsoft.Json;

namespace CoreCodedChatbot.Client.Models
{
    public class TrackModel
    {
        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("album")]
        public string Album { get; set; }

        [JsonProperty("cover")]
        public string CoverUrl { get; set; }

        [JsonProperty("duration")]
        public int DurationSeconds { get; set; }

        [JsonProperty("durationHuman")]
        public string DurationReadable { get; set; }

        [JsonProperty("url")]
        public string WatchUrl { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("isVideo")]
        public bool IsVideo { get; set; }

        [JsonProperty("isAdvertisement")]
        public bool IsAdvert { get; set; }

        [JsonProperty("inLibrary")]
        public bool IsInLibrary { get; set; }
    }
}