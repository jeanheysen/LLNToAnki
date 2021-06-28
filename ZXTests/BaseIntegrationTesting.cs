using LLNToAnki.Business.Logic;
using LLNToAnki.Business.Ports;
using LLNToAnki.Infrastructure.AnkiConnecting;
using LLNToAnki.Infrastructure.HTMLScrapping;
using LLNToAnki.Infrastructure.ReadingWriting;
using LLNToAnki.Infrastructure.URLBuilding;
using Moq;

namespace ZXTests
{
    public class BaseIntegrationTesting : BaseTesting
    {
        protected IDataProvider DataProvider { get; }
        protected ITextSplitter TextSplitter { get; }
        protected IDataWriter FileWriter { get; }
        protected IDataScraper HtmlScraper { get; }
        protected IWordItemBuilder WordItemBuilder { get; }
        protected AnkiNoteExporter AnkiNoteExporter { get; }
        protected AnkiNoteBuilder AnkiNoteBuilder { get; }
        protected ILLNItemsBuilder LLNItemsBuilder { get; }
        protected IConnectNoteBuilder ConnectNoteBuilder { get; }
        protected IConnectNotePoster ConnectNotePoster { get; }
        protected IProcessor Processor { get; }
        
        protected Mock<ITranslationDetailer> TranslationsProviderMock { get; }

        protected string TmpExportFilePath => GetPathInData("tmp_export.txt");


        public BaseIntegrationTesting()
        {
            DataProvider = new FileReader();
            TextSplitter = new TextSplitter();
            
            HtmlScraper = new HTMLScraper();
            WordItemBuilder = new WordItemBuilder(HtmlScraper);
            
            FileWriter = new FileWriter();
            AnkiNoteExporter = new AnkiNoteExporter(FileWriter);

            TranslationsProviderMock = new Mock<ITranslationDetailer>() { DefaultValue = DefaultValue.Mock };
            TranslationsProviderMock.SetupGet(p => p.UrlBuilder).Returns(new WordReferenceURLBuilder());
            AnkiNoteBuilder = new AnkiNoteBuilder(TranslationsProviderMock.Object);

            LLNItemsBuilder = new LLNItemsBuilder();
            ConnectNoteBuilder = new ConnectNoteBuilder();
            ConnectNotePoster = new ConnectNotePoster();

            Processor = new Processor(
                DataProvider,
                LLNItemsBuilder,
                WordItemBuilder,
                AnkiNoteBuilder,
                AnkiNoteExporter,
                ConnectNoteBuilder,
                ConnectNotePoster
                );
        }
        
    }
}