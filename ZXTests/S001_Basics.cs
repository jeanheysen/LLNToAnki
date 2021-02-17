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
    public class Tests
    {
        string squeezeOriginalLLNOutputPath;
        private string tmpExportFilePath;
        private string squeezeExpectedNotePath;
        private string squeezeHtmlOnlyPath;
        private Reader reader;
        private Splitter splitter;
        private FileWriter fileWriter;
        private wordItemBuilder wordItemBuilder;
        private AnkiNoteCsvExporter ankiNoteCsvExporter;
        private AnkiNoteBuilder ankiNoteBuilder;

        [SetUp]
        public void Setup()
        {
            reader = new Reader();
            splitter = new Splitter();
            fileWriter = new FileWriter();
            wordItemBuilder = new wordItemBuilder();
            ankiNoteCsvExporter = new AnkiNoteCsvExporter(fileWriter);
            ankiNoteBuilder = new AnkiNoteBuilder();
        }

        public Tests()
        {
            tmpExportFilePath = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\tmp_export.txt";
            squeezeOriginalLLNOutputPath = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\SingleWord_squeeze.csv";
            squeezeExpectedNotePath = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\SingleWord_squeeze_ExpectedNote.txt";
            squeezeHtmlOnlyPath = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\SingleWord_squeeze_onlyHtml.csv";
        }

        [Test]
        public void T001_TheReaderReadsTheBeginningOfTheFileCorrectly()
        {
            //Act
            string text = new Reader().ReadFileFromPath(squeezeOriginalLLNOutputPath);

            //Assert
            Assert.AreEqual("\"<style>\n\n    html,\n    body {", text.Substring(0, 30));
        }

        [Test]
        public void T002_TheSplitterSplitsTheCsvInThreePieces()
        {
            //Arrange
            string text = new Reader().ReadFileFromPath(squeezeOriginalLLNOutputPath);

            var splittedContent = new Splitter().Split(text);

            Assert.AreEqual(3, splittedContent.Count);
        }

        [Test]
        public void T003_TheHTMLInterpreterPermitsToReturnTheTitleInsideDivDc()
        {
            //Arrange
            HTMLInterpreter hTMLInterpreter = new HTMLInterpreter(new Reader().ReadFileFromPath(squeezeHtmlOnlyPath));
            var extractor = new WordItemExtractor(hTMLInterpreter);

            //Act
            
            var r = extractor.GetTitle();


            Assert.AreEqual("The Crown S4:E2 L'�preuve de Balmoral", r);
        }

        [Test]
        public void T003b_TheInterperterPermitsToReturnTheWordInsideSpanGap()
        {
            //Arrange
            HTMLInterpreter hTMLInterpreter = new HTMLInterpreter(new Reader().ReadFileFromPath(squeezeHtmlOnlyPath));
            var extractor = new WordItemExtractor(hTMLInterpreter);

            //Act

            var r = extractor.GetWord();

            //Assert
            Assert.AreEqual("squeeze", r);
        }

        [Test]
        public void T003c_TheInterpreterReturnsTheTranslation()
        {
            //Arrange
            HTMLInterpreter hTMLInterpreter = new HTMLInterpreter(new Reader().ReadFileFromPath(squeezeHtmlOnlyPath));
            var extractor = new WordItemExtractor(hTMLInterpreter);

            //Act
            var r = extractor.GetTranslation();

            //Assert
            Assert.AreEqual("Et appuyez doucement sur la g�chette.", r);
        }

        [Test]
        public void T004_TheTitleIsInsideTitleOfTheWord()
        {
            //Arrange
            string text = reader.ReadFileFromPath(squeezeOriginalLLNOutputPath);
            var splittedContent = splitter.Split(text);

            //Act
            var item = new wordItemBuilder().Build(splittedContent[0]);

            Assert.AreEqual("The Crown S4:E2 L'�preuve de Balmoral", item.Context.EpisodTitle);
        }

        [Test]
        public void T005_TheWordBuilderReturnsTheWord()
        {
            //Arrange
            string text = new Reader().ReadFileFromPath(squeezeOriginalLLNOutputPath);
            var splittedContent = new Splitter().Split(text);

            //Act
            var word = new wordItemBuilder().Build(splittedContent[0]);

            Assert.AreEqual("squeeze", word.Word);
        }

        [Test]
        public void T007_ExporterSeparatesWithTab()
        {
            //Arrange
            var notes = new AnkiNote()
            {
                Question = "Quelle est la couleur du cheval blanc d'Henri IV ?",
                Answer = "blanc",
                Before="rien avant"
            };

            var fileWriterMock = new Mock<IFileWriter>() { DefaultValue = DefaultValue.Mock };
            var path = "whateverPath";
            var expectedContent = "Quelle est la couleur du cheval blanc d'Henri IV ?	rien avant	blanc";

            //Act
            new AnkiNoteCsvExporter(fileWriterMock.Object).Export(path, notes);


            //Assert
            fileWriterMock.Verify(w => w.Write(path, expectedContent), Times.Once());
        }


        [Test]
        public void T008_GetPartToReplace()
        {
            //Arrange
            HTMLInterpreter hTMLInterpreter = new HTMLInterpreter(new Reader().ReadFileFromPath(squeezeHtmlOnlyPath));
            var extractor = new WordItemExtractor(hTMLInterpreter);

            //Act
            var r = extractor.GetQuestion();

            //Assert
            StringAssert.DoesNotContain("{{c1::", r);
            StringAssert.DoesNotContain("}}", r);
        }

        [Test]
        public void T009_EndToEnd() //TODO � bouger dans une nouvelle suite de test
        {
            //get html
            string text = reader.ReadFileFromPath(squeezeOriginalLLNOutputPath);
            var html = splitter.Split(text)[0];
            var wordItem = wordItemBuilder.Build(html);
            var ankiNote = ankiNoteBuilder.Builder(wordItem);

            //export note
            ankiNoteCsvExporter.Export(this.tmpExportFilePath, ankiNote);

            //Assert
            string expected = reader.ReadFileFromPath(squeezeExpectedNotePath);
            string expectedCleanCarriage = expected.Replace("\r\n", "\n");
            string actual = this.reader.ReadFileFromPath(this.tmpExportFilePath);
            
            Assert.AreEqual(expectedCleanCarriage, actual);
        }
    }
}