using LLNToAnki;
using NUnit.Framework;

namespace ZXTests
{
    public class Tests
    {
        string singleWord_thouURL;

        [SetUp]
        public void Setup()
        {
            singleWord_thouURL = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\singleWord_thou.csv";
        }

        [Test]
        public void T001_ReadLocalFile()
        {
            //Act
            string text = new Reader().Read(singleWord_thouURL);

            //Assert
            Assert.AreEqual("\"<style>\n\n    html,\n    body {", text.Substring(0,30));
        }

        [Test]
        public void T002_SplitStringWithBackSlashT()
        {
            //Arrange
            string text = new Reader().Read(singleWord_thouURL);

            var r = new Splitter().Split(text);

            Assert.AreEqual(3, r.Count);
        }
    }
}