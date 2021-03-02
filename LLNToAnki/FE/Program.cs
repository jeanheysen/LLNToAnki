using LLNToAnki.BE;
using LLNToAnki.BE.Ports;
using LLNToAnki.Infrastructure;
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
            var AnkiNoteBuilder = new AnkiNoteBuilder();
            var LLNItemsBuilder = new LLNItemsBuilder();

            var dataPath = Path.Combine(dataFolder, dataFileName);
            var targetPath = Path.Combine(dataFolder, targetFileName);
            
            var Processor = new Processor(FileReader, LLNItemsBuilder, WordItemBuilder, AnkiNoteBuilder, AnkiNoteExporter);

            var count = Processor.Process(dataPath, targetPath);

            System.Console.WriteLine($"{count} items were generated in {targetPath}");
            System.Console.WriteLine("Press a key to end.");
            System.Console.ReadKey();
        }
    }
}
