using LLNToAnki.BE.Ports;
using System.Collections.Generic;

namespace LLNToAnki.BE
{
    public interface IProcessor
    {
        int Process(string filePath, string targetPath);
    }

    public class Processor : IProcessor
    {
        private readonly IDataProvider dataProvider;
        private readonly ILLNItemsBuilder lLNItemsBuilder;
        private readonly IWordItemBuilder wordItemBuilder;
        private readonly IAnkiNoteBuilder ankiNoteBuilder;
        private readonly IAnkiNoteExporter ankiNoteExporter;

        public Processor(IDataProvider dataProvider,
            ILLNItemsBuilder lLNItemsBuilder,
            IWordItemBuilder wordItemBuilder,
            IAnkiNoteBuilder ankiNoteBuilder,
            IAnkiNoteExporter ankiNoteExporter
            )
        {
            this.dataProvider = dataProvider;
            this.lLNItemsBuilder = lLNItemsBuilder;
            this.wordItemBuilder = wordItemBuilder;
            this.ankiNoteBuilder = ankiNoteBuilder;
            this.ankiNoteExporter = ankiNoteExporter;
        }


        public int Process(string filePath, string targetPath)
        {
            var data = dataProvider.GetAllText(filePath);

            var llnItems = lLNItemsBuilder.Build(data);

            var notes = new List<IAnkiNote>();

            foreach (var item in llnItems)
            {
                var wordItem = wordItemBuilder.Build(item);

                var ankiNote = ankiNoteBuilder.Builder(wordItem);

                notes.Add(ankiNote);
            }

            ankiNoteExporter.Export(targetPath, notes);

            return notes.Count;
        }
    }
}