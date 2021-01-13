using LLNToAnki;
using NUnit.Framework;

namespace ZXTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void T001_ReadLocalFile()
        {
            //Arrange
            string url = @"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\singleWord_thou.csv";

            //Act
            string text = new Reader().Read(url);

            //Assert
            Assert.AreEqual("\"<style>\n\n    html,\n    body {", text.Substring(0,30));
        }
    }
}