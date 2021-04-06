using LLNToAnki.Infrastructure;
using NUnit.Framework;

namespace ZXTests
{
    public class S004_ScrapMijnWordenboek : BaseIntegrationTesting
    {
        private HTMLWebsiteReader htmlreader;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            htmlreader = new HTMLWebsiteReader();
        }

        [Test]
        public void T001_LocalFile_Check26koIsLoaded()
        {
            //Arrange
            var path = GetPathInData(@"MWB\brood.htm");
            
            //Act
            var mainNode = htmlreader.GetHTML(path);

            //Assert
            Assert.Greater(mainNode.InnerLength, 26000);
            Assert.Less(mainNode.InnerLength, 27000);
        }
    }
}
