using LLNToAnki.BE;
using LLNToAnki.BE.Ports;
using LLNToAnki.Infrastructure;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ZXTests
{
    public class S001_Basics : BaseIntegrationTesting
    {
        private Mock<IDataWriter> dataWriterMock;
        
        public S001_Basics()
        {
            dataWriterMock = new Mock<IDataWriter>() { DefaultValue = DefaultValue.Mock };
        }

        [Test]
        public void T001_TheReaderReadsTheBeginningOfTheFileCorrectly()
        {
            //Act
            string text = new FileReader().GetAllText(GetPathInData("SingleWord_squeeze.csv"));

            //Assert
            Assert.AreEqual("\"<style>\n\n    html,\n    body {", text.Substring(0, 30));
        }

        [Test]
        public void T002_TheSplitterSplitsTheCsvInThreePieces()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze.csv"));

            //Act
            var splittedContent = TextSplitter.SplitOnTab(text);

            //Assert
            Assert.AreEqual(3, splittedContent.Count);
        }

        [Test]
        public void T003_TheHTMLInterpreterPermitsToReturnTheTitleInsideDivDc()
        {
            //Arrange
            var html = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));

            //Act
            var r = WordItemBuilder.Build(new LLNItem() { HtmlContent = html });

            //Assert
            Assert.AreEqual("The Crown S4:E2 L'épreuve de Balmoral", r.EpisodTitle);
        }

        [Test]
        public void T004_TheInterperterPermitsToReturnTheWordInsideSpanGap()
        {
            //Arrange
            var html = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));

            //Act
            var r = WordItemBuilder.Build(new LLNItem() { HtmlContent = html });

            //Assert
            Assert.AreEqual("squeeze", r.Word);
        }

        [Test]
        public void T005_TheInterpreterReturnsTheTranslation()
        {
            //Arrange
            var html = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));

            //Act
            var r = WordItemBuilder.Build(new LLNItem() { HtmlContent = html });

            //Assert
            Assert.AreEqual("Et appuyez doucement sur la gâchette.", r.Translation);
        }

        [Test]
        public void T006_TheTitleIsInsideTitleOfTheWord()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze.csv"));
            var splittedContent = TextSplitter.SplitOnTab(text);

            //Act
            var item = WordItemBuilder.Build(new LLNItem() { HtmlContent = splittedContent[0] });

            Assert.AreEqual("The Crown S4:E2 L'épreuve de Balmoral", item.EpisodTitle);
        }

        [Test]
        public void T007_TheWordBuilderReturnsTheWord()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze.csv"));
            var splittedContent = TextSplitter.SplitOnTab(text);

            //Act
            var word = WordItemBuilder.Build(new LLNItem() { HtmlContent = splittedContent[0] });

            Assert.AreEqual("squeeze", word.Word);
        }

        [Test]
        public void T008_ExporterSeparatesWithTab()
        {
            //Arrange
            var ankiNoteMock = new Mock<IAnkiNote>() { DefaultValue = DefaultValue.Mock };
            ankiNoteMock.SetupGet(a => a.Question).Returns("q");
            ankiNoteMock.SetupGet(a => a.Answer).Returns("a");
            ankiNoteMock.SetupGet(a => a.Audio).Returns("ad");
            var fileWriterMock = new Mock<IDataWriter>() { DefaultValue = DefaultValue.Mock };

            //Act
            new AnkiNoteExporter(fileWriterMock.Object).Export("", new List<IAnkiNote>() { ankiNoteMock.Object });


            //Assert
            fileWriterMock.Verify(w => w.Write("", "q	a		ad"), Times.Once());
        }

        [Test]
        public void T009_GetPartToReplace()
        {
            //Arrange
            var html = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));

            //Act
            var r = WordItemBuilder.Build(new LLNItem() { HtmlContent = html });

            //Assert
            StringAssert.DoesNotContain("{{c1::", r.ContextWithWordColored);
            StringAssert.DoesNotContain("}}", r.ContextWithWordColored);
        }

        [Test]
        public void T010_SqueezeEndToEnd()
        {
            //Arrange
            var filePath = GetPathInData("SingleWord_squeeze.csv");

            //Act
            Processor.Process(filePath, TmpExportFilePath);

            //Assert
            string expected = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze_ExpectedNote.txt"));
            string actual = DataProvider.GetAllText(this.TmpExportFilePath);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void T011_WaggingEndToEnd()
        {
            //Arrange
            var filePath = GetPathInData("SingleWord_wagging.csv");

            //Act
            Processor.Process(filePath, TmpExportFilePath);

            //Assert
            string expected = DataProvider.GetAllText(GetPathInData("SingleWord_wagging_ExpectedNote.txt"));
            string actual = this.DataProvider.GetAllText(this.TmpExportFilePath);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void T012_TwoWordsEndToEnd()
        {
            //Arrange
            var filePath = GetPathInData("TwoWords_backbench_disregard.csv");

            //Act
            Processor.Process(filePath, TmpExportFilePath);

            //Assert
            string expected = DataProvider.GetAllText(GetPathInData("TwoWords_backbench_disregard_expected.txt"));
            string actual = this.DataProvider.GetAllText(this.TmpExportFilePath);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void T013_LLNBuilderReturnsTwoItemsForTwoOutputtedWordsInLLN()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("TwoWords_backbench_disregard.csv"));

            //Act
            var llnItems = LLNItemsBuilder.Build(text);

            //Assert
            Assert.AreEqual(2, llnItems.Count);
        }

        [Test]
        public void T014_LLNItemsStartWithHTMLStyleBalise()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("TwoWords_backbench_disregard.csv"));

            //Act
            var llnItems = LLNItemsBuilder.Build(text);

            //Assert
            Assert.AreEqual("\"<style>\n\n    html,\n    body {\n        padding: 0;\n", llnItems[0].HtmlContent.Substring(0, 51));
            Assert.AreEqual("\"<style>\n\n    html,\n    body {\n        padding: 0;\n", llnItems[1].HtmlContent.Substring(0, 51));
        }

        [Test]
        public void T015_NoteBuildsSourceWithWordReference()
        {
            //Arrange
            var item = new Mock<IWordItem>() { DefaultValue = DefaultValue.Mock };
            item.SetupGet(n => n.Word).Returns("bread");

            //Act
            var note = new AnkiNoteBuilder().Build(item.Object);

            //Assert
            Assert.AreEqual($"<a href=\"https://www.wordreference.com/enfr/bread\">https://www.wordreference.com/enfr/bread</a>", note.Source);
        }

        [Test]
        public void T016_ExportedNoteContainsTheSource()
        {
            //Arrange
            var note = new Mock<IAnkiNote>() { DefaultValue = DefaultValue.Mock };
            note.SetupGet(n => n.Source).Returns("https://www.wordreference.com/enfr/pig");
            var exporter = new AnkiNoteExporter(dataWriterMock.Object);

            //Act
            exporter.Export("",new List<IAnkiNote>() { note.Object });

            //Assert
            dataWriterMock.Verify(dw => dw.Write(It.IsAny<string>(), It.Is<string>(c => c.Contains("https://www.wordreference.com/enfr/pig"))));
        }
    }
}