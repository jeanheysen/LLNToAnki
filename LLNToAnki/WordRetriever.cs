using HtmlAgilityPack;
using System.Collections.Generic;

namespace LLNToAnki
{
    public class WordRetriever
    {
        public string GetTitle(string item)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(item);

            var interpreter = new HTMLInterpreter();
            var titleNode = interpreter.GetTitle(htmlDocument);
            return titleNode.LastChild.InnerText;
        }
    }
}