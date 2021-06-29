using LLNToAnki.Domain;

namespace LLNToAnki.Business.Logic
{
    public interface ITargetSequenceBL
    {
        TargetSequence Build(Snapshot snapshot);
    }

    [System.ComponentModel.Composition.Export(typeof(ITargetSequenceBL)), System.Composition.Shared]
    public class TargetSequenceBL : ITargetSequenceBL
    {
        private readonly ITargetSequenceBuilder targetSequenceBuilder;

        [System.ComponentModel.Composition.ImportingConstructor]
        public TargetSequenceBL(ITargetSequenceBuilder targetSequenceBuilder)
        {
            this.targetSequenceBuilder = targetSequenceBuilder;
        }

        public TargetSequence Build(Snapshot snapshot)
        {
            return targetSequenceBuilder.Build(snapshot);
        }
    }
}