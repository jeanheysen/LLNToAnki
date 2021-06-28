using System;
using System.Collections.Generic;
using System.Text;

namespace LLNToAnki.Domain
{
    public class Flow
    {
        public Guid Id { get; set; }

        public List<TargetSequence> TargetSequences { get; set; }
    }
}
