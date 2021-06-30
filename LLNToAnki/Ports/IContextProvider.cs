using LLNToAnki.Domain;
using System.Collections.Generic;

namespace LLNToAnki.Business.Ports
{
    public interface IContextProvider
    {
        IContext Context { get; }
    }

    public interface IContext
    {
        List<Flow> Flows { get; }
        List<TargetSequence> TargetSequences { get; }
    }
}
