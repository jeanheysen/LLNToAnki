using LLNToAnki.Business.Logic;
using LLNToAnki.Business.Ports;
using LLNToAnki.Infrastructure.AnkiConnecting;
using LLNToAnki.Infrastructure.DataStoring;
using LLNToAnki.Infrastructure.HTMLScrapping;
using LLNToAnki.Infrastructure.ReadingWriting;
using LLNToAnki.Infrastructure.URLBuilding;
using Moq;

namespace ZXTests
{
    public class BaseIntegrationTesting : BaseTesting
    {
        protected IDataImporter DataProvider { get; }
        protected IDataWriter FileWriter { get; }
        protected IDataScraper HtmlScraper { get; }
        protected ITargetSequenceBuilder WordItemBuilder { get; }
        protected IAnkiNoteExporter AnkiNoteExporter { get; }
        protected IAnkiNoteBL AnkiNoteBL { get; }
        protected ISnapshotBL LLNItemsBuilder { get; }
        protected IConnectNoteBuilder ConnectNoteBuilder { get; }
        protected IConnectNotePoster ConnectNotePoster { get; }
        protected IContextProvider ContextProvider { get; }
        protected Processor Processor { get; }

        protected Mock<ITranslationDetailer> TranslationsProviderMock { get; }

        protected string TmpExportFilePath => GetPathInData("tmp_export.txt");


        public BaseIntegrationTesting()
        {
            DataProvider = new FileReader();

            HtmlScraper = new HTMLScraper();
            WordItemBuilder = new TargetSequenceBuilder(HtmlScraper);

            FileWriter = new FileWriter();
            AnkiNoteExporter = new AnkiNoteExporter(FileWriter);

            TranslationsProviderMock = new Mock<ITranslationDetailer>() { DefaultValue = DefaultValue.Mock };
            TranslationsProviderMock.SetupGet(p => p.UrlBuilder).Returns(new WordReferenceURLBuilder());
            var builder = new AnkiNoteBuilder(TranslationsProviderMock.Object);
            AnkiNoteBL = new AnkiNoteBL(builder, AnkiNoteExporter);

            LLNItemsBuilder = new SnapshotBL();
            ConnectNoteBuilder = new ConnectNoteBuilder();
            ConnectNotePoster = new ConnectNotePoster();
            ContextProvider = new ContextProvider();

            var snapshotBL = new SnapshotBL();
            var targetSequenceBuilder = new TargetSequenceBuilder(HtmlScraper);
            var targetSequenceBL = new TargetSequenceBL(ContextProvider, targetSequenceBuilder);


            Processor = new Processor(DataProvider, snapshotBL, targetSequenceBL, AnkiNoteBL, ConnectNoteBuilder, ConnectNotePoster);
        }

    }
}