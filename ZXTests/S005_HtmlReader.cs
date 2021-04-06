using LLNToAnki.Infrastructure;
using NUnit.Framework;

namespace ZXTests
{
    public class S005_HtmlReader : BaseIntegrationTesting
    {
        private IHTMLWebsiteReader htmlreader;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            htmlreader = new HTMLWebsiteReader();
        }

        [Test]
        public void T001_CheckLoadFullyIs180ko()
        {
            //Act
            var mainNode = htmlreader.GetHTML(GetPathInData(@"WR/eyeball.html"));

            //Assert
            Assert.Greater(mainNode.InnerLength, 180000);
            Assert.Less(mainNode.InnerLength, 190000);
        }
    }

}
