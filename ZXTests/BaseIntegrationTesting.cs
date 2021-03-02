using LLNToAnki.BE;
using LLNToAnki.BE.Ports;
using LLNToAnki.Infrastructure;
using LLNToAnki.Infrastructure.AnkiConnect;

namespace ZXTests
{
    public class BaseIntegrationTesting
    {
        protected IDataProvider DataProvider { get; }
        protected ITextSplitter TextSplitter { get; }
        protected IDataWriter FileWriter { get; }

        protected IDataScraper HtmlScraper { get; }
        protected IWordItemBuilder WordItemBuilder { get; }
        protected IAnkiNoteExporter AnkiNoteExporter { get; }
        protected IAnkiNoteBuilder AnkiNoteBuilder { get; }
        protected ILLNItemsBuilder LLNItemsBuilder { get; }
        protected IProcessor Processor { get; }

        protected string TmpExportFilePath => GetPathInData("tmp_export.txt");


        public BaseIntegrationTesting()
        {
            DataProvider = new FileReader();

            TextSplitter = new TextSplitter();

            FileWriter = new FileWriter();

            HtmlScraper = new HTMLScraper();

            WordItemBuilder = new WordItemBuilder(HtmlScraper);

            AnkiNoteExporter = new AnkiNoteExporter(FileWriter);

            AnkiNoteBuilder = new AnkiNoteBuilder();

            LLNItemsBuilder = new LLNItemsBuilder();

            Processor = new Processor(
                DataProvider, 
                LLNItemsBuilder, 
                WordItemBuilder, 
                AnkiNoteBuilder, 
                AnkiNoteExporter,
                new ConnectNoteBuilder(),
                new ConnectNotePoster()
                );
        }


        protected string GetPathInData(string fileNameWithExtension)
        {
            return @$"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\{fileNameWithExtension}";
        }
    }
}