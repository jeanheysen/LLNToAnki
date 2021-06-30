using LLNToAnki.Infrastructure;
using LLNToAnki.Infrastructure.HTMLScrapping;
using LLNToAnki.Infrastructure.URLBuilding;
using NUnit.Framework;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ZXTests
{
    public class S005_HtmlReader : BaseIntegrationTesting
    {
        private htmlReader htmlreader;

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
        public void T002_DirectDownloadFromMWBSavesA26KoFileInLessThan2Sec()
        {
            //Arrange
            var urlBuilder = new MijnWordenboekURLBuilder();
            var sw = new Stopwatch();
            sw.Start();

            //Act
            htmlreader.DirectDownload(urlBuilder.CreateURL("brood"), GetPathInTmp("brood.html"));

            //Assert
            sw.Stop();
            Assert.Less(sw.ElapsedMilliseconds, 2000);
            Assert.Greater(File.ReadAllText(GetPathInTmp("brood.html")).Length, 26000);
        }

        [Test]
        public void T003_LocalFile_Check26koIsLoaded()
        {
            //Arrange
            var path = GetPathInData(@"MWB\brood.html");

            //Act
            var mainNode = htmlreader.GetHTML(path);

            //Assert
            Assert.Greater(mainNode.InnerLength, 26000);
            Assert.Less(mainNode.InnerLength, 27000);
        }

        [Test]
        public void T004_ExtractHTMLWithAgilityPackWordReferenceBreadReturnsAbout200koFile()
        {
            //Arrange
            var remoteFilename = @"https://www.wordreference.com/enfr/bread";

            //Act
            var r = htmlreader.GetHTML(remoteFilename);

            //Assert
            Assert.Greater(r.InnerLength, 170000);
            Assert.Less(r.InnerLength, 200000);
        }

        [Test]
        public void T005_RemoveScriptFromTrekken()
        {
            //Arrange
            var remoteFilename = GetPathInData(@"MWB\trekken.html");
            var mainNode = htmlreader.GetHTML(remoteFilename);
            Assert.AreEqual(22, mainNode.Descendants("script").Count());

            //Act
            var cleanedNode = new NodeRemover().Remove(mainNode, "script");

            //Assert
            Assert.LessOrEqual(cleanedNode.Descendants("script").Count(),9);
        }
    }

}
