using HtmlAgilityPack;
using LLNToAnki;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
            string text = new FileReader().ReadAllText(GetPathInData("SingleWord_squeeze.csv"));

            //Act
            var splittedContent = new TextSplitter().SplitOnTab(text);

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
            Assert.AreEqual("The Crown S4:E2 L'�preuve de Balmoral", r.EpisodTitle);
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
            Assert.AreEqual("Et appuyez doucement sur la g�chette.", r.Translation);
        }

        [Test]
        public void T006_TheTitleIsInsideTitleOfTheWord()
        {
            //Arrange
            string text = FileReader.ReadAllText(GetPathInData("SingleWord_squeeze.csv"));
            var splittedContent = TextSplitter.SplitOnTab(text);

            //Act
            var item = WordItemBuilder.Build(splittedContent[0]);

            Assert.AreEqual("The Crown S4:E2 L'�preuve de Balmoral", item.EpisodTitle);
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

            var fileWriterMock = new Mock<IFileWriter>() { DefaultValue = DefaultValue.Mock };
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
        public void T010_SqueezeEndToEnd() //TODO � bouger dans une nouvelle suite de test
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
        public void T011_WaggingEndToEnd() //TODO � bouger dans une nouvelle suite de test
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
        public void T011_TwoWordsEndToEnd() //TODO � bouger dans une nouvelle suite de test
        {
            //Arrange
            string text = FileReader.ReadAllText(GetPathInData("TwoWords_backbench_disregard.csv"));
            var html = TextSplitter.SplitOnTab(text)[0];
            var wordItem = WordItemBuilder.Build(html);
            var ankiNote = AnkiNoteBuilder.Builder(wordItem);
            var html2 = TextSplitter.SplitOnTab(text)[2];
            var wordItem2 = WordItemBuilder.Build(html2);
            var ankiNote2 = AnkiNoteBuilder.Builder(wordItem2);
            var notes = new List<IAnkiNote>() { ankiNote, ankiNote2 };

            //Act
            AnkiNoteExporter.Export(this.TmpExportFilePath, notes);

            //Assert
            string expected = FileReader.ReadAllText(@"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\TwoWords_backbench_disregard_expected.txt");
            string actual = this.FileReader.ReadAllText(this.TmpExportFilePath);
            Assert.AreEqual(expected, actual);
        }
    }
}