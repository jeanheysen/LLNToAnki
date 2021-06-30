﻿using System;

namespace LLNToAnki.Domain
{
    public class Snapshot
    {
        public Guid Id { get; set; }
        
        public string HtmlContent { get; set; }
        
        public string Audio { get; set; }
        
        public string Tag { get; set; }

        public Language Language { get; set; }
    }
}