using System;

namespace CoreCodedChatbot.Client.Models.Rocksniffer
{
    [Serializable]
    public class SnifferOutput
    {
        public SnifferState CurrentState { get; set; }
        public SongDetails SongDetails { get; set; }
        public MemoryReadout MemoryReadout { get; set; }
    }
}