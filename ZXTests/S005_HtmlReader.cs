using LLNToAnki.Infrastructure;
using LLNToAnki.Infrastructure.URLBuilding;
using NUnit.Framework;
using System.Diagnostics;
using System.IO;

namespace ZXTests
{
    public class S005_HtmlReader : BaseIntegrationTesting
    {
        private IHTMLWebsiteReader htmlreader;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            htmlreader = new HTMLWebsiteReader();
            ClearTmp();
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

        [Test]
        public void T002_DirectDownloadFromMWBSavesA26KoFileInLessThanASec()
        {
            //Arrange
            var urlBuilder = new MijnWordenboekURLBuilder();
            var sw = new Stopwatch();
            sw.Start();

            //Act
            htmlreader.DirectDownload(urlBuilder.CreateURL("brood"), GetPathInTmp("brood.html"));

            //Assert
            sw.Stop();
            Assert.Less(sw.ElapsedMilliseconds, 1000);
            Assert.Greater(File.ReadAllText(GetPathInTmp("brood.html")).Length, 26000);
        }
    }

}
