using LLNToAnki.BE.Enums;
using LLNToAnki.BE.Ports;
using LLNToAnki.Infrastructure;
using LLNToAnki.Infrastructure.HTMLScrapping;
using LLNToAnki.Infrastructure.URLBuilding;
using Moq;
using NUnit.Framework;

namespace ZXTests
{
    public class S004_ScrapMijnWordenboek : BaseIntegrationTesting
    {
        private ITranslationDetailer detailer;


        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var urlBuilderMock = new Mock<IURLBuilder>() { DefaultValue = DefaultValue.Mock };
            urlBuilderMock.Setup(b => b.CreateURL(It.IsAny<string>())).Returns<string>(s => GetPathInData(@"MWB\" + s + ".html"));

            var urlBuilderFactoryMock = new Mock<IUrlLAbstractFactory>() { DefaultValue = DefaultValue.Mock };
            urlBuilderFactoryMock.Setup(f => f.CreateUrlBuilder(It.IsAny<Language>())).Returns(urlBuilderMock.Object);

            detailer = new MijnWordenboekDetailer(urlBuilderMock.Object, new HTMLScraper(), new HTMLWebsiteReader());
        }

        [TestCase("wit en bruin brood")]
        [TestCase("du pain blanc et du pain gris")]
        [TestCase("broden")]
        public void T001_ExtractPrincipalTranslationFromPage(string txt)
        {
            //act
            var node = detailer.GetAll("brood");

            //Assert
            StringAssert.Contains(txt, node);
        }
    }
}
