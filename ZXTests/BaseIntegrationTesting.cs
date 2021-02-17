using LLNToAnki.BE;
using LLNToAnki.BE.Ports;
using LLNToAnki.Infrastructure;

namespace ZXTests
{
    public class BaseIntegrationTesting
    {
        protected IDataProvider FileReader { get; private set; }
        protected ITextSplitter TextSplitter { get; private set; }
        protected IDataWriter FileWriter { get; private set; }

        protected IDataScraper HtmlScraper { get; private set; }
        protected IWordItemBuilder WordItemBuilder { get; private set; }
        protected IAnkiNoteExporter AnkiNoteExporter { get; private set; }
        protected IAnkiNoteBuilder AnkiNoteBuilder { get; private set; }

        protected string TmpExportFilePath => GetPathInData("tmp_export.txt");


        public BaseIntegrationTesting()
        {
            FileReader = new FileReader();
            
            TextSplitter = new TextSplitter();
            
            FileWriter = new FileWriter();

            HtmlScraper = new HTMLScraper();
            
            WordItemBuilder = new WordItemBuilder(HtmlScraper);
            
            AnkiNoteExporter = new AnkiNoteExporter(FileWriter);
            
            AnkiNoteBuilder = new AnkiNoteBuilder();
        }


        protected string GetPathInData(string fileNameWithExtension)
        {
            return @$"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\{fileNameWithExtension}";
        }
    }
}