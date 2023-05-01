using System;

namespace CoreCodedChatbot.Client.Models.Rocksniffer
{
    [Serializable]
    public class NoteData
    {
        public float Accuracy { get; set; }
        public int TotalNotes { get; set; }
        public int TotalNotesHit { get; set; }
        public int CurrentHitStreak { get; set; }
        public int HighestHitStream { get; set; }
        public int TotalNotesMissed { get; set; }
        public int CurrentMissStreak { get; set; }
    }
}