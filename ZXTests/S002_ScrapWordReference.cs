using HtmlAgilityPack;
using LLNToAnki.Infrastructure;
using LLNToAnki.Infrastructure.HTMLScrapping;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ZXTests
{
    public class S002_ScrapWordReference
    {
        string localWordReferenceEyball = @"C:\Users\felix\source\repos\LLNToAnki\Dictionaries\WordReference\eyeball - English-French Dictionary WordReference.com.htm";
        private HTMLWebsiteReader htmlreader;
        private WordReferenceTranslationsProvider wordReferenceTranslationProvider;
        private Mock<IURLBuilder> urlBuilderMock;

        private string LocalWordReferenceURL(string word)
        {
            return @$"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\WR\WR_{word}.html";
        }

        public S002_ScrapWordReference()
        {
            htmlreader = new HTMLWebsiteReader();

            urlBuilderMock = new Mock<IURLBuilder>() { DefaultValue = DefaultValue.Mock };

            urlBuilderMock.Setup(b => b.OnlineWordReference(It.IsAny<string>())).Callback<string>(s => LocalWordReferenceURL(s));

            wordReferenceTranslationProvider = new WordReferenceTranslationsProvider(urlBuilderMock.Object);
        }
        
        [Test]
        public void T001_LoadEyeBallWordReferenceFully()
        {
            //Act
            var mainNode = htmlreader.GetHTML(localWordReferenceEyball);

            //Assert
            Assert.Greater(mainNode.InnerLength, 120000);
        }

        [Test]
        public void T002_ExtractPrincipalTranslationFromPage()
        {
            //Arrange
            var mainNode = htmlreader.GetHTML(localWordReferenceEyball);
            var scraper = new HTMLScraper();

            //act
            var node = scraper.GetNodeByNameAndAttribute(mainNode, "table", "class");

            //Assert
            StringAssert.Contains("The human eyeball is not perfectly spherical.", mainNode.InnerText);
            StringAssert.Contains("globe oculaire", node.InnerText);
        }

        [Test]
        public void T003_DownloadPageMinjWoordenBoekForBreadReturnsA26koFile()
        {
            //Arrange
            var remoteFilename = @"https://www.mijnwoordenboek.nl/vertaal/NL/FR/brood";
            var localFilename = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\WR\MWBbrood.htm";
            if (File.Exists(localFilename)) File.Delete(localFilename);

            //Act
            htmlreader.DirectDownload(remoteFilename, localFilename);

            //Assert
            Assert.Greater(File.ReadAllText(localFilename).Length, 26000);
        }

        [Test]
        public void T004_DirectDownloadWordReferenceThrowsException()
        {
            //Arrange
            var remoteFilename = @"https://www.wordreference.com/enfr/bread";
            var localFilename = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\WR\WRbread.htm";
            if (File.Exists(localFilename)) File.Delete(localFilename);

            //Assert
            Assert.Throws<System.Net.WebException>(()=> htmlreader.DirectDownload(remoteFilename, localFilename));
        }

        [Test]
        public void T005_ExtractHTMLWithAgilityPackWordReferenceBreadReturns174koFile()
        {
            //Arrange
            var remoteFilename = @"https://www.wordreference.com/enfr/bread";

            //Act
            var r = htmlreader.GetHTML(remoteFilename);

            //Assert
            Assert.Greater(r.InnerLength, 174000);
        }

        [Test]
        public void T006_LoadEyeBallWordReferenceFully()
        {
            //Act
            var r = wordReferenceTranslationProvider.GetTranslations("eyeball");

            //Assert
            StringAssert.DoesNotContain(": Refers to person, place, thing, quality, etc.", r);
        }
    }
}
