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
    public class Tests : BaseIntegrationTesting
    {
        string squeezeOriginalLLNOutputPath;
        string twoWordsOriginalLLNOutputPath;
        string waggingOriginalLLNOutputPath;
        private string tmpExportFilePath;
        private string squeezeHtmlOnlyPath;
        private WordItemBuilder wordItemBuilder;
        private AnkiNoteExporter ankiNoteCsvExporter;
        private AnkiNoteBuilder ankiNoteBuilder;

        [SetUp]
        public void Setup()
        {
            fileReader = new FileReader();
            splitter = new TextSplitter();
            fileWriter = new FileWriter();
            wordItemBuilder = new WordItemBuilder();
            ankiNoteCsvExporter = new AnkiNoteExporter(fileWriter);
            ankiNoteBuilder = new AnkiNoteBuilder();
        }

        public Tests()
        {
            tmpExportFilePath = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\tmp_export.txt";
            squeezeOriginalLLNOutputPath = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\SingleWord_squeeze.csv";
            twoWordsOriginalLLNOutputPath = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\TwoWords_backbench_disregard.csv";
            waggingOriginalLLNOutputPath = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\SingleWord_wagging.csv";
            squeezeHtmlOnlyPath = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\SingleWord_squeeze_onlyHtml.csv";
        }

        [Test]
        public void T001_TheReaderReadsTheBeginningOfTheFileCorrectly()
        {
            //Act
            string text = new FileReader().ReadAllText(squeezeOriginalLLNOutputPath);

            //Assert
            Assert.AreEqual("\"<style>\n\n    html,\n    body {", text.Substring(0, 30));
        }

        [Test]
        public void T002_TheSplitterSplitsTheCsvInThreePieces()
        {
            //Arrange
            string text = new FileReader().ReadAllText(squeezeOriginalLLNOutputPath);

            var splittedContent = new TextSplitter().SplitOnTab(text);

            Assert.AreEqual(3, splittedContent.Count);
        }

        [Test]
        public void T003_TheHTMLInterpreterPermitsToReturnTheTitleInsideDivDc()
        {
            //Arrange
            var html = fileReader.ReadAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));
            var extractor = new WordItemExtractor(new HTMLInterpreter());

            //Act
            var r = extractor.GetTitle(html);


            Assert.AreEqual("The Crown S4:E2 L'épreuve de Balmoral", r);
        }

        [Test]
        public void T003b_TheInterperterPermitsToReturnTheWordInsideSpanGap()
        {
            //Arrange
            var html = fileReader.ReadAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));
            var extractor = new WordItemExtractor(new HTMLInterpreter());

            //Act
            var r = extractor.GetWord(html);

            //Assert
            Assert.AreEqual("squeeze", r);
        }

        [Test]
        public void T003c_TheInterpreterReturnsTheTranslation()
        {
            //Arrange
            var html = fileReader.ReadAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));
            var extractor = new WordItemExtractor(new HTMLInterpreter());

            //Act
            var r = extractor.GetTranslation(html);

            //Assert
            Assert.AreEqual("Et appuyez doucement sur la gâchette.", r);
        }

        [Test]
        public void T004_TheTitleIsInsideTitleOfTheWord()
        {
            //Arrange
            string text = fileReader.ReadAllText(squeezeOriginalLLNOutputPath);
            var splittedContent = splitter.SplitOnTab(text);

            //Act
            var item = new WordItemBuilder().Build(splittedContent[0]);

            Assert.AreEqual("The Crown S4:E2 L'épreuve de Balmoral", item.Context.EpisodTitle);
        }

        [Test]
        public void T005_TheWordBuilderReturnsTheWord()
        {
            //Arrange
            string text = new FileReader().ReadAllText(squeezeOriginalLLNOutputPath);
            var splittedContent = new TextSplitter().SplitOnTab(text);

            //Act
            var word = new WordItemBuilder().Build(splittedContent[0]);

            Assert.AreEqual("squeeze", word.Word);
        }

        [Test]
        public void T007_ExporterSeparatesWithTab()
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
        public void T008_GetPartToReplace()
        {
            //Arrange
            var html = fileReader.ReadAllText(GetPathInData("SingleWord_squeeze_onlyHtml.csv"));
            var extractor = new WordItemExtractor(new HTMLInterpreter());

            //Act
            var r = extractor.GetQuestion(html);

            //Assert
            StringAssert.DoesNotContain("{{c1::", r);
            StringAssert.DoesNotContain("}}", r);
        }

        [Test]
        public void T009_SqueezeEndToEnd() //TODO à bouger dans une nouvelle suite de test
        {
            //get html
            string text = fileReader.ReadAllText(squeezeOriginalLLNOutputPath);
            var html = splitter.SplitOnTab(text)[0];
            var wordItem = wordItemBuilder.Build(html);
            var note = ankiNoteBuilder.Builder(wordItem);

            //export note
            ankiNoteCsvExporter.Export(this.tmpExportFilePath, new List<AnkiNote>() { note });

            //Assert
            string expected = fileReader.ReadAllText(GetPathInData("SingleWord_squeeze_ExpectedNote.txt"));
            string actual = this.fileReader.ReadAllText(this.tmpExportFilePath);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void T010_WaggingEndToEnd() //TODO à bouger dans une nouvelle suite de test
        {
            //get html
            string text = fileReader.ReadAllText(waggingOriginalLLNOutputPath);
            var html = splitter.SplitOnTab(text)[0];
            var wordItem = wordItemBuilder.Build(html);
            var note = ankiNoteBuilder.Builder(wordItem);

            //export note
            ankiNoteCsvExporter.Export(this.tmpExportFilePath, new List<AnkiNote>() { note });

            //Assert
            string expected = fileReader.ReadAllText(GetPathInData("SingleWord_wagging_ExpectedNote.txt"));
            string actual = this.fileReader.ReadAllText(this.tmpExportFilePath);
            Assert.AreEqual(expected, actual);
        }

        

        [Test]
        public void T011_TwoWordsEndToEnd() //TODO à bouger dans une nouvelle suite de test
        {
            //Arrange
            string text = fileReader.ReadAllText(twoWordsOriginalLLNOutputPath);
            var html = splitter.SplitOnTab(text)[0];
            var wordItem = wordItemBuilder.Build(html);
            var ankiNote = ankiNoteBuilder.Builder(wordItem);
            var html2 = splitter.SplitOnTab(text)[2];
            var wordItem2 = wordItemBuilder.Build(html2);
            var ankiNote2 = ankiNoteBuilder.Builder(wordItem2);
            var notes = new List<AnkiNote>() { ankiNote, ankiNote2 };

            //Act
            ankiNoteCsvExporter.Export(this.tmpExportFilePath, notes);

            //Assert
            string expected = fileReader.ReadAllText(@"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\TwoWords_backbench_disregard_expected.txt");
            string actual = this.fileReader.ReadAllText(this.tmpExportFilePath);
            Assert.AreEqual(expected, actual);
        }
    }
}