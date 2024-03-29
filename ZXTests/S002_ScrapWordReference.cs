﻿using HtmlAgilityPack;
using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
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
        private ITranslationDetailer wrDetailsProvider;
        

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var urlBuilderMock = new Mock<IURLBuilder>() { DefaultValue = DefaultValue.Mock };
            urlBuilderMock.Setup(b => b.CreateURL(It.IsAny<string>())).Returns<string>(s => GetPathInData(@"WR\" + s + ".html"));

            wrDetailsProvider = new WordReferenceDetailer(urlBuilderMock.Object, new HTMLScraper(), new HTMLWebsiteReader());
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

        [TestCase("eyeball")]
        [TestCase("concurs")]
        public void T002_ParasitsGrammarExplanationAreRemovedFromWordReferenceExplanations(string fileName)
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
