using System;
using System.Collections.Generic;

namespace LLNToAnki.Facade.Dto
{
    public class FlowDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<TargetSequenceDto> TargetSequences { get; set; }
    }
}
