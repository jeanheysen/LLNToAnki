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
        public S001_Basics()
        {
        }

        [Test]
        public void T001_TheReaderReadsTheBeginningOfTheFileCorrectly()
        {
            //Act
            string text = new FileReader().ReadAllText(GetPathInData("SingleWord_squeeze.csv"));

            //Assert
            Assert.AreEqual("\"<style>\n\n    html,\n    body {", text.Substring(0, 30));
        }

        [Test]
        public void T002_TheSplitterSplitsTheCsvInThreePieces()
        {
            //Arrange
            string text = FileReader.ReadAllText(GetPathInData("SingleWord_squeeze.csv"));

            //Act
            var splittedContent = TextSplitter.SplitOnTab(text);

            //Assert
            Assert.AreEqual(3, splittedContent.Count);
        }

        [Test]
        public void T003_TheHTMLInterpreterPermitsToReturnTheTitleInsideDivDc()
        {
            //Arrange
            var html = FileReader.ReadAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));

            //Act
            var r = WordItemBuilder.Build(html);

            //Assert
            Assert.AreEqual("The Crown S4:E2 L'épreuve de Balmoral", r.EpisodTitle);
        }

        [Test]
        public void T004_TheInterperterPermitsToReturnTheWordInsideSpanGap()
        {
            //Arrange
            var html = FileReader.ReadAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));

            //Act
            var r = WordItemBuilder.Build(html);

            //Assert
            Assert.AreEqual("squeeze", r.Word);
        }

        [Test]
        public void T005_TheInterpreterReturnsTheTranslation()
        {
            //Arrange
            var html = FileReader.ReadAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));

            //Act
            var r = WordItemBuilder.Build(html);

            //Assert
            Assert.AreEqual("Et appuyez doucement sur la gâchette.", r.Translation);
        }

        [Test]
        public void T006_TheTitleIsInsideTitleOfTheWord()
        {
            //Arrange
            string text = FileReader.ReadAllText(GetPathInData("SingleWord_squeeze.csv"));
            var splittedContent = TextSplitter.SplitOnTab(text);

            //Act
            var item = WordItemBuilder.Build(splittedContent[0]);

            Assert.AreEqual("The Crown S4:E2 L'épreuve de Balmoral", item.EpisodTitle);
        }

        [Test]
        public void T007_TheWordBuilderReturnsTheWord()
        {
            //Arrange
            string text = FileReader.ReadAllText(GetPathInData("SingleWord_squeeze.csv"));
            var splittedContent = TextSplitter.SplitOnTab(text);

            //Act
            var word = WordItemBuilder.Build(splittedContent[0]);

            Assert.AreEqual("squeeze", word.Word);
        }

        [Test]
        public void T008_ExporterSeparatesWithTab()
        {
            //Arrange
            var note = new AnkiNote()
            {
                Question = "Quelle est la couleur du cheval blanc d'Henri IV ?",
                Answer = "blanc"
            };

            var fileWriterMock = new Mock<IDataWriter>() { DefaultValue = DefaultValue.Mock };
            var path = "whateverPath";
            var expectedContent = "Quelle est la couleur du cheval blanc d'Henri IV ?	blanc";

            //Act
            new AnkiNoteExporter(fileWriterMock.Object).Export(path, new List<AnkiNote>() { note });


            //Assert
            fileWriterMock.Verify(w => w.Write(path, expectedContent), Times.Once());
        }

        [Test]
        public void T009_GetPartToReplace()
        {
            //Arrange
            var html = FileReader.ReadAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));

            //Act
            var r = WordItemBuilder.Build(html);

            //Assert
            StringAssert.DoesNotContain("{{c1::", r.ContextWithWordColored);
            StringAssert.DoesNotContain("}}", r.ContextWithWordColored);
        }

        [Test]
        public void T010_SqueezeEndToEnd() //TODO à bouger dans une nouvelle suite de test
        {
            //get html
            string text = FileReader.ReadAllText(GetPathInData("SingleWord_squeeze.csv"));
            var html = TextSplitter.SplitOnTab(text)[0];
            var wordItem = WordItemBuilder.Build(html);
            var note = AnkiNoteBuilder.Builder(wordItem);

            //export note
            AnkiNoteExporter.Export(this.TmpExportFilePath, new List<IAnkiNote>() { note });

            //Assert
            string expected = FileReader.ReadAllText(GetPathInData("SingleWord_squeeze_ExpectedNote.txt"));
            string actual = this.FileReader.ReadAllText(this.TmpExportFilePath);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void T011_WaggingEndToEnd() //TODO à bouger dans une nouvelle suite de test
        {
            //get html
            string text = FileReader.ReadAllText(GetPathInData("SingleWord_wagging.csv"));
            var html = TextSplitter.SplitOnTab(text)[0];
            var wordItem = WordItemBuilder.Build(html);
            var note = AnkiNoteBuilder.Builder(wordItem);

            //export note
            AnkiNoteExporter.Export(this.TmpExportFilePath, new List<IAnkiNote>() { note });

            //Assert
            string expected = FileReader.ReadAllText(GetPathInData("SingleWord_wagging_ExpectedNote.txt"));
            string actual = this.FileReader.ReadAllText(this.TmpExportFilePath);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void T012_TwoWordsEndToEnd()
        {
            //Arrange
            string text = FileReader.ReadAllText(GetPathInData("TwoWords_backbench_disregard.csv"));
            var llnItems = lLNItemsBuilder.Build(text);

            var notes = new List<IAnkiNote>() { };
            foreach (var item in llnItems)
            {
                var wordItem = WordItemBuilder.Build(item.HtmlContent);
                var ankiNote = AnkiNoteBuilder.Builder(wordItem);
                notes.Add(ankiNote);
            }

            //Act
            AnkiNoteExporter.Export(this.TmpExportFilePath, notes);

            //Assert
            string expected = FileReader.ReadAllText(@"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\TwoWords_backbench_disregard_expected.txt");
            string actual = this.FileReader.ReadAllText(this.TmpExportFilePath);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void T013_LLNBuilderReturnsTwoItemsForTwoOutputtedWordsInLLN()
        {
            //Arrange
            string text = FileReader.ReadAllText(GetPathInData("TwoWords_backbench_disregard.csv"));

            //Act
            var llnItems = lLNItemsBuilder.Build(text);

            //Assert
            Assert.AreEqual(2, llnItems.Count);
        }

        [Test]
        public void T014_LLNItemsStartWithHTMLStyleBalise()
        {
            //Arrange
            string text = FileReader.ReadAllText(GetPathInData("TwoWords_backbench_disregard.csv"));

            //Act
            var llnItems = lLNItemsBuilder.Build(text);

            //Assert
            Assert.AreEqual("\"<style>\n\n    html,\n    body {\n        padding: 0;\n", llnItems[0].HtmlContent.Substring(0, 51));
            Assert.AreEqual("\"<style>\n\n    html,\n    body {\n        padding: 0;\n", llnItems[1].HtmlContent.Substring(0, 51));
        }
    }
}