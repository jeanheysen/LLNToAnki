using LLNToAnki.Business.Ports;
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
        private readonly IContextProvider contextProvider;

        //services
        private readonly ITargetSequenceBuilder targetSequenceBuilder;

        //constructor
        [System.ComponentModel.Composition.ImportingConstructor]
        public TargetSequenceBL(IContextProvider contextProvider, ITargetSequenceBuilder targetSequenceBuilder)
        {
            this.contextProvider = contextProvider;
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