using LLNToAnki.Business.Logic;
using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
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
        protected IAnkiNoteBuilder AnkiNoteBuilder { get; }

        protected IAnkiNoteBL AnkiNoteBL { get; }
        protected ISnapshotBL SnapshotBL { get; }
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

            var dictionaryFactoryMock = new Mock<IDictionaryAbstractFactory>() { DefaultValue = DefaultValue.Mock };
            dictionaryFactoryMock.Setup(d => d.Provide(It.IsAny<Language>())).Returns(TranslationsProviderMock.Object);

            AnkiNoteBuilder = new AnkiNoteBuilder(dictionaryFactoryMock.Object);
            AnkiNoteBL = new AnkiNoteBL(AnkiNoteBuilder, AnkiNoteExporter);

            ContextProvider = new ContextProvider();
            var languageBL = new LanguageBL(ContextProvider);
            SnapshotBL = new SnapshotBL(ContextProvider, languageBL);
            ConnectNoteBuilder = new ConnectNoteBuilder();
            ConnectNotePoster = new ConnectNotePoster();

            var targetSequenceBuilder = new TargetSequenceBuilder(HtmlScraper);
            var targetSequenceBL = new TargetSequenceBL(ContextProvider, targetSequenceBuilder, AnkiNoteBuilder, ConnectNoteBuilder, ConnectNotePoster);


            Processor = new Processor(DataProvider, SnapshotBL, targetSequenceBL, AnkiNoteBL, ConnectNoteBuilder, ConnectNotePoster);
        }

    }
}