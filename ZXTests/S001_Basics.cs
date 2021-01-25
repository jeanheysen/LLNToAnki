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
        string singleWord_squeezeURL;

        [SetUp]
        public void Setup()
        {
            singleWord_squeezeURL = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\SingleWord_squeeze.csv";
        }

        [Test]
        public void T001_ReadLocalFile()
        {
            //Act
            string text = new Reader().Read(singleWord_squeezeURL);

            //Assert
            Assert.AreEqual("\"<style>\n\n    html,\n    body {", text.Substring(0, 30));
        }

        [Test]
        public void T002_SplitStringWithBackSlashT()
        {
            //Arrange
            string text = new Reader().Read(singleWord_squeezeURL);

            var splittedContent = new Splitter().Split(text);

            Assert.AreEqual(3, splittedContent.Count);
        }

        [Test]
        public void T003_HTMLInterpreter_TitleIsInsideDivDcTitleAttribute()
        {
            //Arrange
            var htmlOnlyURL = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\SingleWord_squeeze_onlyHtml.csv";
            string text = new Reader().Read(htmlOnlyURL);
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(text);

            //Act
            var titleNode = new HTMLInterpreter(htmlDoc).GetNodeByNameAndAttribute("div", "dc-title\"\"");


            Assert.AreEqual("The Crown S4:E2 L'épreuve de Balmoral", titleNode.LastChild.InnerText);
        }

        [Test]
        public void T003b_HTMLInterpreter_WordIsInsideDcGap()
        {
            //Arrange
            var htmlOnlyURL = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\SingleWord_squeeze_onlyHtml.csv";
            string text = new Reader().Read(htmlOnlyURL);
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(text);

            //Act
            var node = new HTMLInterpreter(htmlDoc).GetNodeByNameAndAttribute("span", "dc-gap\"\"");

            //Assert
            Assert.AreEqual("squeeze", node.LastChild.InnerText);
        }

        [Test]
        public void T003c_HTMLInterpreter_TranslationIsInDivDcLineDcTranslation()
        {
            //Arrange
            var htmlOnlyURL = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\SingleWord_squeeze_onlyHtml.csv";
            string text = new Reader().Read(htmlOnlyURL);
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(text);

            //Act
            var node = new HTMLInterpreter(htmlDoc).GetNodeByNameAndAttribute("div", "dc-translation");

            //Assert
            Assert.AreEqual("Et appuyez doucement sur la gâchette.", node.LastChild.InnerText);
        }

        [Test]
        public void T004_RetrieveTheTitle()
        {
            //Arrange
            string text = new Reader().Read(singleWord_squeezeURL);
            var splittedContent = new Splitter().Split(text);

            //Act
            var word = new WordBuilder().GetWord(splittedContent[0]);

            Assert.AreEqual("The Crown S4:E2 L'épreuve de Balmoral", word.EpisodTitle);
        }

        [Test]
        public void T005_RetrieveTheWordSqueeze()
        {
            //Arrange
            string text = new Reader().Read(singleWord_squeezeURL);
            var splittedContent = new Splitter().Split(text);

            //Act
            var word = new WordBuilder().GetWord(splittedContent[0]);

            Assert.AreEqual("squeeze", word.Text);
        }

        [Test]
        public void T006_GetTheHTMLQuestionWithNoFrenchInIt()
        {
            //Arrange
            string text = new Reader().Read(singleWord_squeezeURL);
            var splittedContent = new Splitter().Split(text);
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(text);

            //Act
            var question = new QuestionBuilder().Build(htmlDoc.DocumentNode);

            //Assert
            Assert.That(question, Does.Contain("squeeze"));
            Assert.That(question, Does.Contain("The Crown S4:E2 L'épreuve de Balmoral"));
            Assert.That(question, Does.Not.Contain("Et appuyez doucement sur la gâchette."));
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
        public void T008_EndToEnd()
        {
            //get html
            var htmlDoc = new HtmlDocument();
            Reader reader = new Reader();
            string text = reader.Read(this.singleWord_squeezeURL);
            var splittedContent = new Splitter().Split(text);
            htmlDoc.LoadHtml(splittedContent[0]);

            //build word
            var word = new WordBuilder().GetWord(splittedContent[0]);

            //build question
            var question = new QuestionBuilder().Build(htmlDoc.DocumentNode);


            //build note
            var ankinote = new AnkiNote()
            {
                Question=question,
                Answer = word.Text,
                Before=""
            };

            //export note
            var exportFilePath = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\tmp_export.txt";
            new AnkiNoteCsvExporter(new FileWriter()).Export(exportFilePath, ankinote);

            var reffilePath = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\SingleWord_squeeze_ExpectedNote.txt";
            Assert.AreEqual(reader.Read(reffilePath), reader.Read(exportFilePath));
        }
    }
}