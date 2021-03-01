using HtmlAgilityPack;
using LLNToAnki.Infrastructure;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ZXTests
{
    public class S002_ScrapWordReference
    {
        string localWordReferenceEyball = @"C:\Users\felix\source\repos\LLNToAnki\Dictionaries\WordReference\eyeball - English-French Dictionary WordReference.com.htm";

        public HtmlDocument CreateHtmlDocument(string url)
        {
            var web = new HtmlWeb();

            return web.Load(url);
        }

        [Test]
        public void T001_LoadEyeBallWordReferenceFully()
        {
            //Act
            var document = CreateHtmlDocument(localWordReferenceEyball);

            //Assert
            Assert.Greater(document.DocumentNode.InnerLength, 120000);
        }

        [Test]
        public void T002_ExtractPrincipalTranslationFromPage()
        {
            //Arrange
            var document = CreateHtmlDocument(localWordReferenceEyball);
            var scraper = new HTMLScraper();

            //act
            var node = scraper.GetNodeByNameAndAttribute(document.DocumentNode, "table", "class");

            //Assert
            StringAssert.Contains("The human eyeball is not perfectly spherical.", node.InnerText);
            StringAssert.Contains("globe oculaire", node.InnerText);
        }
    }
}
