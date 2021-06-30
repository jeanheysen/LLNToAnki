using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
using System.Collections.Generic;
using System.Text;

namespace LLNToAnki.Business.Logic
{
    public interface IAnkiNoteBL
    {
        AnkiNote Create(TargetSequence item);
        void Export(string path, IReadOnlyList<AnkiNote> notes);
    }

    [System.ComponentModel.Composition.Export(typeof(IAnkiNoteBL)), System.Composition.Shared]
    public class AnkiNoteBL : IAnkiNoteBL
    {
        private readonly IAnkiNoteBuilder ankiNoteBuilder;
        private readonly IAnkiNoteExporter ankiNoteExporter;

        public AnkiNoteBL(IAnkiNoteBuilder ankiNoteBuilder, IAnkiNoteExporter ankiNoteExporter)
        {
            this.ankiNoteBuilder = ankiNoteBuilder;
            this.ankiNoteExporter = ankiNoteExporter;
        }

        public AnkiNote Create(TargetSequence item)
        {
            return ankiNoteBuilder.Create(item);
        }

        public void Export(string path, IReadOnlyList<AnkiNote> notes)
        {
            this.ankiNoteExporter.Export(path, notes);
        }
    }
}