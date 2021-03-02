﻿using LLNToAnki.BE;
using LLNToAnki.BE.Ports;
using LLNToAnki.Infrastructure;
using LLNToAnki.Infrastructure.AnkiConnect;
using LLNToAnki.Infrastructure.HTMLScrapping;
using System.IO;

namespace LLNToAnki.FE
{
    class Program
    {
        private const string dataFolder = @"C:\Tmp";
        private const string dataFileName = @"items.csv";
        private const string targetFileName = @"ankiNotes.txt";

        static void Main(string[] args)
        {
            var FileReader = new FileReader();
            var FileWriter = new FileWriter();
            var HtmlScraper = new HTMLScraper();
            var WordItemBuilder = new WordItemBuilder(HtmlScraper);
            var AnkiNoteExporter = new AnkiNoteExporter(FileWriter);
            var AnkiNoteBuilder = new AnkiNoteBuilder(new WordReferenceTranslationsProvider());
            var LLNItemsBuilder = new LLNItemsBuilder();

            var dataPath = Path.Combine(dataFolder, dataFileName);
            var targetPath = Path.Combine(dataFolder, targetFileName);
            
            var Processor = new Processor(
                FileReader, 
                LLNItemsBuilder, 
                WordItemBuilder, 
                AnkiNoteBuilder, 
                AnkiNoteExporter,
                new ConnectNoteBuilder(),
                new ConnectNotePoster()
                );

            //var count = Processor.WriteInTextFile(dataPath, targetPath);
            //System.Console.WriteLine($"{count} items were generated in {targetPath}");

            var count = Processor.PushToAnkiThroughAPI(dataPath);
            System.Console.WriteLine($"{count} items were directly added to Anki through API.");

            System.Console.WriteLine("Press a key to end.");
            System.Console.ReadKey();
        }
    }
}
