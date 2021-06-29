using System;

namespace LLNToAnki.Domain
{
    public class TargetSequence
    {
        public Guid Id{ get; set; }
        
        public string Sequence { get; set; }

        public string EpisodTitle { get; set; }

        public string ContextWithWordColored { get; set; }

        public string Translation { get; set; }

        public string Audio { get; set; }

        public Snapshot SnapShot { get; set; }
    }
}
