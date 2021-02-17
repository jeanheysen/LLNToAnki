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

        public static IDataProvider FileReader { get; private set; }
        public static ITextSplitter TextSplitter { get; private set; }
        public static IDataWriter FileWriter { get; private set; }
        public static IDataScraper HtmlScraper { get; private set; }
        public static IWordItemBuilder WordItemBuilder { get; private set; }
        public static IAnkiNoteExporter AnkiNoteExporter { get; private set; }
        public static IAnkiNoteBuilder AnkiNoteBuilder { get; private set; }
        public static IProcessor Processor { get; private set; }
        public static ILLNItemsBuilder LLNItemsBuilder { get; private set; }


        static void Main(string[] args)
        {
            FileReader = new FileReader();
            TextSplitter = new TextSplitter();
            FileWriter = new FileWriter();
            HtmlScraper = new HTMLScraper();
            WordItemBuilder = new WordItemBuilder(HtmlScraper);
            AnkiNoteExporter = new AnkiNoteExporter(FileWriter);
            AnkiNoteBuilder = new AnkiNoteBuilder();
            LLNItemsBuilder = new LLNItemsBuilder();

            var dataPath = Path.Combine(dataFolder, dataFileName);
            var targetPath = Path.Combine(dataFolder, targetFileName);
            
            Processor = new Processor(FileReader, LLNItemsBuilder, WordItemBuilder, AnkiNoteBuilder, AnkiNoteExporter);

            var count = Processor.Process(dataPath, targetPath);

            System.Console.WriteLine($"{count} items were generated in {targetPath}");
            System.Console.WriteLine("Press a key to end.");
            System.Console.ReadKey();
        }
    }
}
