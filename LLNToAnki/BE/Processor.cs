using LLNToAnki.BE.Ports;
using System;
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
                try
                {
                    var wordItem = wordItemBuilder.Build(item);

                    var ankiNote = ankiNoteBuilder.Build(wordItem);

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

            var llnItems = lLNItemsBuilder.Build(data);

            var totalCount = nbOfItemsToParse != 0 ? nbOfItemsToParse : llnItems.Count;
            var i = 0;

            foreach (var item in llnItems)
            {
                try
                {
                    var wordItem = wordItemBuilder.Build(item);

                    var ankiNote = ankiNoteBuilder.Build(wordItem);

                    var connectNote = connectNoteBuilder.Build(ankiNote);

                    var body = connectNotePoster.Post(connectNote).Result;

                    System.Console.WriteLine($"{++i} / {totalCount}. Word : {wordItem.Word}. ({body})");

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