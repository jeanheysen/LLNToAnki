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

            var titleNode = new HTMLInterpreter(htmlDocument).GetNodeByNameAndAttribute("div", "dc-title\"\"");

            return titleNode.LastChild.InnerText;
        }
    }
}