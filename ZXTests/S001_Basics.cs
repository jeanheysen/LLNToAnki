using LLNToAnki.Business.Logic;
using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
using LLNToAnki.Infrastructure;
using LLNToAnki.Infrastructure.HTMLScrapping;
using LLNToAnki.Infrastructure.ReadingWriting;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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
            Assert.AreEqual("\"<style>", text.Substring(0, 8));
        }

        [Test]
        public void T002_TheSplitterSplitsTheCsvInThreePieces()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze.csv"));

            //Act
            var splittedContent = text.Split("\t").ToList();

            //Assert
            Assert.AreEqual(3, splittedContent.Count);
        }

        [Test]
        public void T003_TheHTMLInterpreterPermitsToReturnTheTitleInsideDivDc()
        {
            //Arrange
            var html = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));

            //Act
            var r = WordItemBuilder.Build(new Snapshot() { HtmlContent = html });

            //Assert
            Assert.AreEqual("The Crown S4:E2 L'épreuve de Balmoral", r.EpisodTitle);
        }

        [Test]
        public void T004_TheInterperterPermitsToReturnTheWordInsideSpanGap()
        {
            //Arrange
            var html = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));

            //Act
            var r = WordItemBuilder.Build(new Snapshot() { HtmlContent = html });

            //Assert
            Assert.AreEqual("squeeze", r.Sequence);
        }

        [Test]
        public void T005_TheInterpreterReturnsTheTranslation()
        {
            //Arrange
            var html = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));

            //Act
            var r = WordItemBuilder.Build(new Snapshot() { HtmlContent = html });

            //Assert
            Assert.AreEqual("Et appuyez doucement sur la gâchette.", r.Translation);
        }

        [Test]
        public void T006_TheTitleIsInsideTitleOfTheWord()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze.csv"));
            var splittedContent = text.Split("\t").ToList();

            //Act
            var item = WordItemBuilder.Build(new Snapshot() { HtmlContent = splittedContent[0] });

            Assert.AreEqual("The Crown S4:E2 L'épreuve de Balmoral", item.EpisodTitle);
        }

        [Test]
        public void T007_TheWordBuilderReturnsTheWord()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze.csv"));
            var splittedContent = text.Split("\t").ToList();

            //Act
            var word = WordItemBuilder.Build(new Snapshot() { HtmlContent = splittedContent[0] });

            Assert.AreEqual("squeeze", word.Sequence);
        }

        [Test]
        public void T008_ExporterSeparatesWithTab()
        {
            //Arrange
            var ankiNote = new AnkiNote();
            ankiNote.Question = "q";
            ankiNote.Answer = "a";
            ankiNote.Audio = "ad";
            var fileWriterMock = new Mock<IDataWriter>() { DefaultValue = DefaultValue.Mock };

            //Act
            new AnkiNoteExporter(fileWriterMock.Object).Export("", new List<AnkiNote>() { ankiNote });


            //Assert
            fileWriterMock.Verify(w => w.Write("", "\"q\"	a			ad"), Times.Once());
        }

        [Test]
        public void T009_GetPartToReplace()
        {
            //Arrange
            var html = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));

            //Act
            var r = WordItemBuilder.Build(new Snapshot() { HtmlContent = html });

            //Assert
            StringAssert.DoesNotContain("{{c1::", r.ContextWithWordColored);
            StringAssert.DoesNotContain("}}", r.ContextWithWordColored);
        }

        [Test]
        public void T013_LLNBuilderReturnsTwoItemsForTwoOutputtedWordsInLLN()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("TwoWords_backbench_disregard.csv"));

            //Act
            var llnItems = LLNItemsBuilder.Create(text);

            //Assert
            Assert.AreEqual(2, llnItems.Count);
        }

        [Test]
        public void T015_NoteBuildsSourceWithWordReference()
        {
            //Arrange
            var item = new TargetSequence { Sequence = "bread" };

            //Act
            var note = AnkiNoteBL.Create(item);

            //Assert
            Assert.AreEqual($"<a href=\"https://www.wordreference.com/enfr/bread\">https://www.wordreference.com/enfr/bread</a>", note.Source);
        }

        [Test]
        public void T016_ExportedNoteContainsTheSource()
        {
            //Arrange
            var note = new AnkiNote();
            note.Question = "";
            note.Source = "https://www.wordreference.com/enfr/pig";
            var exporter = new AnkiNoteExporter(dataWriterMock.Object);

            //Act
            exporter.Export("", new List<AnkiNote>() { note });

            //Assert
            dataWriterMock.Verify(dw => dw.Write(It.IsAny<string>(), It.Is<string>(c => c.Contains("https://www.wordreference.com/enfr/pig"))));
        }

        [Test]
        public void T017_NoteBuildsAfterWithTranslation()
        {
            //Arrange
            var item = new TargetSequence { Translation = "c'est la traduction" };

            //Act
            var note = AnkiNoteBL.Create(item);

            //Assert
            Assert.AreEqual("Traduction Netflix : \"c'est la traduction\".", note.After);
        }
    }
}