using LLNToAnki.BE.Ports;
using System.Collections.Generic;
using System.Linq;

namespace LLNToAnki.BE
{
    public interface IProcessor
    {
        int WriteInTextFile(string filePath, string targetPath);
    }

    public class Processor : IProcessor
    {
        private readonly IDataProvider dataProvider;
        private readonly ILLNItemsBuilder lLNItemsBuilder;
        private readonly IWordItemBuilder wordItemBuilder;
        private readonly IAnkiNoteBuilder ankiNoteBuilder;
        private readonly IAnkiNoteExporter ankiNoteExporter;
        private readonly IConnectNoteBuilder connectNoteBuilder;
        private readonly IConnectNotePoster connectNotePoster;

        public Processor(IDataProvider dataProvider,
            ILLNItemsBuilder lLNItemsBuilder,
            IWordItemBuilder wordItemBuilder,
            IAnkiNoteBuilder ankiNoteBuilder,
            IAnkiNoteExporter ankiNoteExporter,
            IConnectNoteBuilder connectNoteBuilder,
            IConnectNotePoster connectNotePoster
            )
        {
            this.dataProvider = dataProvider;
            this.lLNItemsBuilder = lLNItemsBuilder;
            this.wordItemBuilder = wordItemBuilder;
            this.ankiNoteBuilder = ankiNoteBuilder;
            this.ankiNoteExporter = ankiNoteExporter;
            this.connectNoteBuilder = connectNoteBuilder;
            this.connectNotePoster = connectNotePoster;
        }


        public int WriteInTextFile(string filePath, string targetPath)
        {
            List<IAnkiNote> notes = CreateAnkiNotes(filePath);

            ankiNoteExporter.Export(targetPath, notes);

            return notes.Count;
        }

        private List<IAnkiNote> CreateAnkiNotes(string filePath)
        {
            var notes = new List<IAnkiNote>();

            var data = dataProvider.GetAllText(filePath);

            var llnItems = lLNItemsBuilder.Build(data);

            foreach (var item in llnItems)
            {
                var wordItem = wordItemBuilder.Build(item);

                var ankiNote = ankiNoteBuilder.Build(wordItem);

                notes.Add(ankiNote);
            }

            return notes;
        }

        public int PushToAnkiThroughAPI(string filePath)
        {
            List<IAnkiNote> notes = CreateAnkiNotes(filePath);

            var connectNote = connectNoteBuilder.Build(notes.First());
            
            connectNotePoster.Post(connectNote).Wait();

            return notes.Count;
        }
    }
}