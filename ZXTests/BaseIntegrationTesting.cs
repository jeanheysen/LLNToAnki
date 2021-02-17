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
        protected IFileReader FileReader { get; private set; }
        protected ITextSplitter Splitter { get; private set; }
        protected IFileWriter FileWriter { get; private set; }
        protected string TmpExportFilePath => GetPathInData("tmp_export.txt");


        public BaseIntegrationTesting()
        {
            FileReader = new FileReader();
            Splitter = new TextSplitter();
            FileWriter = new FileWriter();
        }


        protected string GetPathInData(string fileNameWithExtension)
        {
            return @$"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\{fileNameWithExtension}";
        }
    }
}