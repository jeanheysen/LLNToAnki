using HtmlAgilityPack;
using LLNToAnki;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ZXTests
{
    public class BaseIntegrationTesting
    {
        protected IFileReader fileReader;
        protected ITextSplitter splitter;
        protected IFileWriter fileWriter;

        public BaseIntegrationTesting()
        {

        }


        protected string GetPathInData(string fileNameWithExtension)
        {
            return @$"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\{fileNameWithExtension}";
        }
    }
}