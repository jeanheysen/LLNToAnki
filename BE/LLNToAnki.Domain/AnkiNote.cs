﻿namespace LLNToAnki.Domain
{
    public class AnkiNote
    {
        public string Question { get; set; }
        public string Before { get; set; }
        public string Answer { get; set; }
        public string After { get; set; }
        public string Source { get; set; }
        public string Audio { get; set; }
        public string Mem_Image { get; set; }
        public string Mem_Text { get; set; }
        public string AddReverseQuestion { get; set; }
        public string AddReverseAnswer { get; set; }
    }
}