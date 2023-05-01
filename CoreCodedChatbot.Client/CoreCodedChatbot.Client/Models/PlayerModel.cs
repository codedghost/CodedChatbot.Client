using Newtonsoft.Json;

namespace CoreCodedChatbot.Client.Models
{
    public class PlayerModel
    {
        [JsonProperty("hasSong")]
        public bool HasSong { get; set; }

        [JsonProperty("isPaused")]
        public bool IsPaused { get; set; }

        [JsonProperty("volumePercent")]
        public float VolumePercent { get; set; }

        [JsonProperty("seekbarCurrentPosition")]
        public int SeekbarCurrentPositionSeconds { get; set; }

        [JsonProperty("seekbarCurrentPositionHuman")]
        public string SeekbarCurrentPositionReadable { get; set; }

        [JsonProperty("statePercent")]
        public float StatePercent { get; set; }

        [JsonProperty("likeStatus")]
        public string LikedStatus { get; set; }

        [JsonProperty("repeatType")]
        public string RepeatType { get; set; }
    }
}