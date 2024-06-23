using System;
using System.Collections.Generic;

namespace CoreCodedChatbot.Client.Models.Rocksniffer
{
    [Serializable]
    public class SongDetails
    {
        public string songID;
        public string songName;
        public string artistName;
        public string albumName;

        public float songLength = 0;

        public int albumYear = 0;
        public int NumArrangements { get { return arrangements.Count; } }

        public List<ArrangementDetails> arrangements = new List<ArrangementDetails>();

        public ToolkitDetails toolkit;
        
        public string psarcFileHash;
        

        /// <summary>
        /// Returns true if this SongDetails object seems valid (has valid field values)
        /// </summary>
        /// <returns>True if SongDetails seems valid</returns>
        public bool IsValid()
        {
            return !(songLength == 0 && albumYear == 0 && NumArrangements == 0);
        }
    }
}