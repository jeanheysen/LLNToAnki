using HtmlAgilityPack;
using LLNToAnki;
using LLNToAnki.BE;
using LLNToAnki.Infrastructure;
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
        protected ITextSplitter TextSplitter { get; private set; }
        protected IFileWriter FileWriter { get; private set; }

        protected IHTMLScraper HtmlScraper { get; private set; }
        protected IWordItemBuilder WordItemBuilder { get; private set; }
        protected IAnkiNoteExporter AnkiNoteExporter { get; private set; }
        protected IAnkiNoteBuilder AnkiNoteBuilder { get; private set; }

        protected string TmpExportFilePath => GetPathInData("tmp_export.txt");


        public BaseIntegrationTesting()
        {
            FileReader = new FileReader();
            
            TextSplitter = new TextSplitter();
            
            FileWriter = new FileWriter();

            HtmlScraper = new HTMLScraper();
            
            WordItemBuilder = new WordItemBuilder(HtmlScraper);
            
            AnkiNoteExporter = new AnkiNoteExporter(FileWriter);
            
            AnkiNoteBuilder = new AnkiNoteBuilder();
        }


        protected string GetPathInData(string fileNameWithExtension)
        {
            return @$"C:\Users\felix\source\repos\LLNToAnki\ZXTests\Data\{fileNameWithExtension}";
        }
    }
}