using LLNToAnki.Business.Logic;
using System.Collections.Generic;

namespace LLNToAnki.Facade.Controllers
{
    public class TargetSequenceController
    {
        private readonly ITargetSequenceBL targetSequenceBL;

        public TargetSequenceController(ITargetSequenceBL targetSequenceBL)
        {
            this.targetSequenceBL = targetSequenceBL;
        }
    }
}
