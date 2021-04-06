using LLNToAnki.Infrastructure;
using NUnit.Framework;

namespace ZXTests
{
    public class S004_ScrapMijnWordenboek : BaseIntegrationTesting
    {
        string localHtmlPath = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\WR\MWBbrood.htm";
        private HTMLWebsiteReader htmlreader;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            htmlreader = new HTMLWebsiteReader();
        }

        [Test]
        public void T001_LocalMWBBrood_Check26koIsLoaded()
        {
            //Arrange
            var path = GetPathInData(@"WR\MWBbrood.htm");
            
            //Act
            var mainNode = htmlreader.GetHTML(path);

            //Assert
            Assert.AreEqual(mainNode.InnerLength, 26690);
        }
    }
}
