using LLNToAnki.Business.Logic;
using System;
using System.Collections.Generic;

namespace LLNToAnki.Facade.Controllers
{
    public interface ITargetSequenceController
    {
        void PostToAnki(Guid id);
    }

    public class TargetSequenceController : ITargetSequenceController
    {
        //dependencies
        private readonly ITargetSequenceBL targetSequenceBL;

        //constructor
        public TargetSequenceController(ITargetSequenceBL targetSequenceBL)
        {
            this.targetSequenceBL = targetSequenceBL;
        }

        //method
        public void PostToAnki(Guid id)
        {
            targetSequenceBL.PostToAnki(id);
        }
    }
}
