using LLNToAnki.Domain;
using System;

namespace LLNToAnki.Business.Logic
{
    public interface ITargetSequenceBL
    {
        TargetSequence Build(Snapshot snapshot);
        void PostToAnki(Guid id);
    }

    [System.ComponentModel.Composition.Export(typeof(ITargetSequenceBL)), System.Composition.Shared]
    public class TargetSequenceBL : ITargetSequenceBL
    {
        //services
        private readonly ITargetSequenceBuilder targetSequenceBuilder;

        //constructor
        [System.ComponentModel.Composition.ImportingConstructor]
        public TargetSequenceBL(ITargetSequenceBuilder targetSequenceBuilder)
        {
            this.targetSequenceBuilder = targetSequenceBuilder;
        }

        //method
        public TargetSequence Build(Snapshot snapshot)
        {
            return targetSequenceBuilder.Build(snapshot);
        }

        public void PostToAnki(Guid id)
        {

        }
    }
}