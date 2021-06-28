using LLNToAnki.BE;
using LLNToAnki.Domain;
using LLNToAnki.Infrastructure.AnkiConnecting;
using LLNToAnki.Infrastructure.HTMLScrapping;
using LLNToAnki.Infrastructure.ReadingWriting;
using LLNToAnki.Infrastructure.URLBuilding;
using System;
using System.IO;

namespace LLNToAnki.Console
{
    class Program
    {
        private const string dataFolder = @"C:\Tmp";
        private const string dataFileName = @"items.csv";
        private const string targetFileName = @"ankiNotes.txt";

        static void Main(string[] args)
        {
            var total = InputNumberOfItemsToParse();
            var language = InputLanguage();
            var detailerFactory = new DetailerFactory(
                new UrlAbstractFactory(),
                    new HTMLScraper(),
                    new HTMLWebsiteReader());

            var detailer = detailerFactory.Provide(language);

            var processor = new Processor(
                new FileReader(),
                new LLNItemsBuilder(),
                new WordItemBuilder(new HTMLScraper()),
                new AnkiNoteBuilder(detailer),
                new AnkiNoteExporter(new FileWriter()),
                new ConnectNoteBuilder(),
                new ConnectNotePoster()
                );

            var dataPath = Path.Combine(dataFolder, dataFileName);
            var count = processor.PushToAnkiThroughAPI(dataPath, total);

            EndProcessWithMessage(total, count);
        }

        private static void EndProcessWithMessage(int total, int count)
        {
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine();
            var totalToDisplay = total != 0 ? total : count;
            System.Console.WriteLine($"{totalToDisplay } items were directly added to Anki through API.");

            System.Console.WriteLine("Press a key to end.");
            System.Console.ReadKey();
        }

        private static int InputNumberOfItemsToParse()
        {
            System.Console.WriteLine("Please enter number of words to add : (0 or enter if all)");
            var response = System.Console.ReadLine();
            int.TryParse(response, out int nbOfItemsToParse);
            return nbOfItemsToParse;
        }

        private static Language InputLanguage()
        {
            System.Console.WriteLine("english (en) or dutch (nl) ? default or unrecknownized character means english.");
            var response = System.Console.ReadLine();

            if (response == "en") return Language.English;
            if (response == "nl") return Language.Dutch;

            return Language.English;
        }
    }

}
