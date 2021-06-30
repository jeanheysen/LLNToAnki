using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LLNToAnki.Infrastructure.DataStoring
{
    public class Context : IContext
    {
        public List<Flow> Flows { get; }

        public List<TargetSequence> TargetSequences => Flows.SelectMany(f => f.TargetSequences).ToList();

        public Context()
        {
            Flows = new List<Flow>();
        }
    }
}
