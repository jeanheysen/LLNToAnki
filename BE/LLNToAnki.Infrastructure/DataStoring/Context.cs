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
        
        public List<Snapshot> Snapshots => Flows.SelectMany(f => f.TargetSequences).Select(s=>s.SnapShot).ToList();

        public List<Language> Languages { get; }

        public Context()
        {
            Flows = new List<Flow>();

            Languages = new List<Language>();

            Languages.Add(new Language() {Id = Guid.NewGuid(), Name = "Dutch" });
            
            Languages.Add(new Language() {Id = Guid.NewGuid(), Name = "English" });
        }
    }
}
