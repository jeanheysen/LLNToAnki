using LLNToAnki.BE;
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
            var urlBuilder = new URLBuilder();
            var translationsProvider = new WordReferenceDetailsProvider(urlBuilder, HtmlScraper);
            var AnkiNoteBuilder = new AnkiNoteBuilder(translationsProvider);
            var LLNItemsBuilder = new LLNItemsBuilder();

            var dataPath = Path.Combine(dataFolder, dataFileName);
            var targetPath = Path.Combine(dataFolder, targetFileName);
            
            var processor = new Processor(
                FileReader, 
                LLNItemsBuilder, 
                WordItemBuilder, 
                AnkiNoteBuilder, 
                AnkiNoteExporter,
                new ConnectNoteBuilder(),
                new ConnectNotePoster()
                );

            System.Console.WriteLine("Please enter number of words to add : (0 or enter if all)");
            var response = System.Console.ReadLine();
            int nbOfItemsToParse = 0;
            int.TryParse(response, out nbOfItemsToParse);
            var count = processor.PushToAnkiThroughAPI(dataPath, nbOfItemsToParse);
            System.Console.WriteLine($"{count} items were directly added to Anki through API.");

            System.Console.WriteLine("Press a key to end.");
            System.Console.ReadKey();
        }
    }
}
