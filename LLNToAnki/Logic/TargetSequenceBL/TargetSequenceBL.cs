using LLNToAnki.Domain;

namespace LLNToAnki.Business.Logic
{
    public class TargetSequenceBL
    {
        private readonly ITargetSequenceBuilder targetSequenceBuilder;

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