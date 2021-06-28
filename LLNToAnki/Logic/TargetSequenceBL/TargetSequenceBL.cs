using LLNToAnki.Domain;

namespace LLNToAnki.Business.Logic
{
    public interface ITargetSequenceBL
    {
        TargetSequence Build(Snapshot snapshot);
    }

    public class TargetSequenceBL : ITargetSequenceBL
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