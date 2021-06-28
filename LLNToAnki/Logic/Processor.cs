using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LLNToAnki.Business.Logic
{
    public interface IProcessor
    {
        int WriteInTextFile(string filePath, string targetPath);
    }

    public class Processor : IProcessor
    {
        private readonly IDataProvider dataProvider;
        private readonly ISnapshotBL lLNItemsBuilder;
        private readonly IWordItemBuilder wordItemBuilder;
        private readonly IAnkiNoteBL ankiNoteBuilder;
        private readonly IAnkiNoteExporter ankiNoteExporter;
        private readonly IConnectNoteBuilder connectNoteBuilder;
        private readonly IConnectNotePoster connectNotePoster;

        public Processor(IDataProvider dataProvider,
            ISnapshotBL lLNItemsBuilder,
            IWordItemBuilder wordItemBuilder,
            IAnkiNoteBL ankiNoteBuilder,
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
            List<AnkiNote> notes = CreateAnkiNotes(filePath);

            ankiNoteExporter.Export(targetPath, notes);

            return notes.Count;
        }

        private List<AnkiNote> CreateAnkiNotes(string filePath)
        {
            var notes = new List<AnkiNote>();

            var data = dataProvider.GetAllText(filePath);

            var llnItems = lLNItemsBuilder.Create(data);

            foreach (var item in llnItems)
            {
                try
                {
                    var wordItem = wordItemBuilder.Build(item);

                    var ankiNote = ankiNoteBuilder.Create(wordItem);

                    notes.Add(ankiNote);
                }
                finally
                {

                }
               
            }

            return notes;
        }

        public int PushToAnkiThroughAPI(string filePath, int nbOfItemsToParse)
        {
            var data = dataProvider.GetAllText(filePath);

            var llnItems = lLNItemsBuilder.Create(data);

            var totalCount = nbOfItemsToParse != 0 ? nbOfItemsToParse : llnItems.Count;
            var i = 0;

            foreach (var item in llnItems)
            {
                try
                {
                    var wordItem = wordItemBuilder.Build(item);

                    var ankiNote = ankiNoteBuilder.Create(wordItem);

                    var connectNote = connectNoteBuilder.Build(ankiNote);

                    var body = connectNotePoster.Post(connectNote).Result;

                    System.Console.WriteLine($"{++i} / {totalCount}. Word : {wordItem.Sequence}. ({body})");

                    if (i == nbOfItemsToParse) break;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return llnItems.Count;
        }
    }
}