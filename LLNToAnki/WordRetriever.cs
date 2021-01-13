using HtmlAgilityPack;
using System.Collections.Generic;

namespace LLNToAnki
{
    public class WordRetriever
    {
        public Word GetWord(string item)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(item);

            var hTMLInterpreter = new HTMLInterpreter(htmlDocument);
            var titleNode = hTMLInterpreter.GetNodeByNameAndAttribute("div", "dc-title\"\"");

            return new Word() { EpisodTitle = titleNode.LastChild.InnerText };
        }
    }
}