using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LLNToAnki.Business.Logic
{
    public interface ITargetSequenceBL
    {
        TargetSequence Build(Snapshot snapshot);
        Task PostToAnki(Guid id);
    }

    [System.ComponentModel.Composition.Export(typeof(ITargetSequenceBL)), System.Composition.Shared]
    public class TargetSequenceBL : ITargetSequenceBL
    {
        private readonly IContextProvider contextProvider;

        //services
        private readonly ITargetSequenceBuilder targetSequenceBuilder;
        private readonly IAnkiNoteBuilder ankiNoteBuilder;
        private readonly IConnectNoteBuilder connectNoteBuilder;
        private readonly IConnectNotePoster connectNotePoster;

        //constructor
        [System.ComponentModel.Composition.ImportingConstructor]
        public TargetSequenceBL(
            IContextProvider contextProvider, 
            ITargetSequenceBuilder targetSequenceBuilder, 
            IAnkiNoteBuilder ankiNoteBuilder,
            IConnectNoteBuilder connectNoteBuilder,
            IConnectNotePoster connectNotePoster)
        {
            this.contextProvider = contextProvider;
            this.targetSequenceBuilder = targetSequenceBuilder;
            this.ankiNoteBuilder = ankiNoteBuilder;
            this.connectNoteBuilder = connectNoteBuilder;
            this.connectNotePoster = connectNotePoster;
        }

        //method
        public TargetSequence Build(Snapshot snapshot)
        {
            return targetSequenceBuilder.Build(snapshot);
        }

        public async Task PostToAnki(Guid id)
        {
            var wordItem = contextProvider.Context.TargetSequences.First(t => t.Id == id);

            var ankiNote = ankiNoteBuilder.Create(wordItem);

            var connectNote = connectNoteBuilder.Build(ankiNote);

            var body = await connectNotePoster.Post(connectNote);
        }
    }
}