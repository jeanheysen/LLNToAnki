using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LLNToAnki.Business.Logic
{
    public interface IFlowBL
    {
        Flow GetById(Guid id);
        Guid Create(string path);
    }

    [System.ComponentModel.Composition.Export(typeof(IFlowBL)), System.Composition.Shared]
    public class FlowBL : IFlowBL
    {
        //fields
        private List<Flow> flows;

        //services
        private readonly IDataProvider dataProvider;
        private readonly ISnapshotBL snapshotBL;
        private readonly ITargetSequenceBL targetSequenceBL;

        [System.ComponentModel.Composition.ImportingConstructor]
        public FlowBL(IDataProvider dataProvider, ISnapshotBL snapshotBL, ITargetSequenceBL targetSequenceBL)
        {
            flows = new List<Flow>();
            this.dataProvider = dataProvider;
            this.snapshotBL = snapshotBL;
            this.targetSequenceBL = targetSequenceBL;
        }

        public Guid Create(string path)
        {
            var id = Guid.NewGuid();

            var flow = new Flow() { Id = id };

            flows.Add(flow);

            flow.TargetSequences = CreateSequences(path);

            return id;
        }

        public List<TargetSequence> CreateSequences(string filePath)
        {
            var data = dataProvider.GetAllText(filePath);

            var llnItems = snapshotBL.Create(data);

            var sequences = new List<TargetSequence>();

            foreach (var item in llnItems)
            {
                sequences.Add(targetSequenceBL.Build(item));
            }

            return sequences;
        }


        public Flow GetById(Guid id)
        {
            return flows.FirstOrDefault(f => f.Id == id);
        }
    }
}