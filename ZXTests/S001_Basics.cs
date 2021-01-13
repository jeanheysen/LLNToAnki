using HtmlAgilityPack;
using LLNToAnki;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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
        public void T004_RetrieveTheTitle()
        {
            //Arrange
            string text = new Reader().Read(singleWord_squeezeURL);
            var splittedContent = new Splitter().Split(text);

            //Act
            var title = new WordRetriever().GetTitle(splittedContent[0]);

            Assert.AreEqual("The Crown S4:E2 L'épreuve de Balmoral", title);
        }
    }
}