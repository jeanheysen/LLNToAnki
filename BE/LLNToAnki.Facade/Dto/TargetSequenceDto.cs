using System;
using System.Text;

namespace LLNToAnki.Facade.Dto
{
    public class TargetSequenceDto
    {
        public Guid Id { get; set; }

        public string Sequence { get; set; }

        public string EpisodTitle { get; set; }

        public SnapshotDto Snapshot { get; set; }
    }
}
