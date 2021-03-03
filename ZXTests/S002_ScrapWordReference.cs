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

        private string LocalWordReferenceURL(string fileName)
        {
            return @$"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\WR\{fileName}";
        }

        public S002_ScrapWordReference()
        {
            htmlreader = new HTMLWebsiteReader();

            urlBuilderMock = new Mock<IURLBuilder>() { DefaultValue = DefaultValue.Mock };

            urlBuilderMock.Setup(b => b.OnlineWordReference(It.IsAny<string>())).Returns<string>(s => LocalWordReferenceURL(s));

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
            Assert.Throws<System.Net.WebException>(() => htmlreader.DirectDownload(remoteFilename, localFilename));
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

        [TestCase("WR_eyeball.html")]
        [TestCase("WR_concurs.htm")]
        public void T006_ParasitsGrammarExplanationAreRemovedFromWordReferenceExplanations(string fileName)
        {
            //Act
            var r = wordReferenceTranslationProvider.GetTranslations(fileName);

            //Assert
            StringAssert.DoesNotContain(": Refers to person, place, thing, quality, etc.", r);
            StringAssert.DoesNotContain("devant une voyelle ou un h muet", r);
            StringAssert.DoesNotContain("Verb taking a direct object", r);
            StringAssert.DoesNotContain("verbe qui s'utilise avec un complément d'objet direct (COD)", r);
            //StringAssert.DoesNotContain("Is something important missing? Report an error or suggest an improvement.", r);

            StringAssert.DoesNotContain("Verb not taking a direct object--for example", r);
            StringAssert.DoesNotContain("verbe qui s'utilise sans complément d'objet direct (COD)", r);
            StringAssert.DoesNotContain("verbe qui s'utilise avec le pronom réfléchi", r);
        }
    }
}
