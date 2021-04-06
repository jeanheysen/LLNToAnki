using HtmlAgilityPack;
using LLNToAnki.BE.Enums;
using LLNToAnki.BE.Ports;
using LLNToAnki.Infrastructure;
using LLNToAnki.Infrastructure.HTMLScrapping;
using LLNToAnki.Infrastructure.URLBuilding;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ZXTests
{
    public class S002_ScrapWordReference : BaseIntegrationTesting
    {
        private IHTMLWebsiteReader htmlreader;
        private ITranslationDetailsProvider wrDetailsProvider;

        private Mock<IURLBuilder> urlBuilderMock;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            urlBuilderMock = new Mock<IURLBuilder>() { DefaultValue = DefaultValue.Mock };
            urlBuilderMock.Setup(b => b.CreateURL(It.IsAny<string>())).Returns<string>(s => GetPathInData(@"WR\" + s + ".html"));

            var urlBuilderFactoryMock = new Mock<IUrlLAbstractFactory>() { DefaultValue = DefaultValue.Mock };
            urlBuilderFactoryMock.Setup(f => f.CreateUrlBuilder(It.IsAny<Language>())).Returns(urlBuilderMock.Object);

            htmlreader = new HTMLWebsiteReader();
            wrDetailsProvider = new WordReferenceDetailsProvider(urlBuilderFactoryMock.Object, new HTMLScraper(), new HTMLWebsiteReader());
        }

        [TestCase("The human eyeball is not perfectly spherical.")]
        [TestCase("globe oculaire")]
        public void T001_ExtractPrincipalTranslationFromPage(string txt)
        {
            //act
            var node = wrDetailsProvider.GetAll("eyeball");

            //Assert
            StringAssert.Contains(txt, node);
        }

        [Test]
        public void T002_ExtractHTMLWithAgilityPackWordReferenceBreadReturns174koFile()
        {
            //Arrange
            var remoteFilename = @"https://www.wordreference.com/enfr/bread";

            //Act
            var r = htmlreader.GetHTML(remoteFilename);

            //Assert
            Assert.Greater(r.InnerLength, 174000);
            Assert.Less(r.InnerLength, 180000);
        }

        [TestCase("eyeball")]
        [TestCase("concurs")]
        public void T006_ParasitsGrammarExplanationAreRemovedFromWordReferenceExplanations(string fileName)
        {
            //Act
            var r = wrDetailsProvider.GetAll(fileName);

            //Assert
            StringAssert.DoesNotContain(": Refers to person, place, thing, quality, etc.", r);
            StringAssert.DoesNotContain("devant une voyelle ou un h muet", r);
            StringAssert.DoesNotContain("Verb taking a direct object", r);
            StringAssert.DoesNotContain("verbe qui s'utilise avec un complément d'objet direct (COD)", r);
            StringAssert.DoesNotContain("Verb not taking a direct object--for example", r);
            StringAssert.DoesNotContain("verbe qui s'utilise sans complément d'objet direct (COD)", r);
            StringAssert.DoesNotContain("verbe qui s'utilise avec le pronom réfléchi", r);
        }
    }

}
