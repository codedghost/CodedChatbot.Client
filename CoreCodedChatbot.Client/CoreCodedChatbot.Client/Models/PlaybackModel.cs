using Newtonsoft.Json;

namespace CoreCodedChatbot.Client.Models
{
    public class PlaybackModel
    {
        [JsonProperty("player")]
        public PlayerModel PlayerModel { get; set; }

        [JsonProperty("track")]
        public TrackModel TrackModel { get; set; }
    }
}