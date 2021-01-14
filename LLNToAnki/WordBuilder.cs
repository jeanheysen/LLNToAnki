using HtmlAgilityPack;
using System.Collections.Generic;

namespace LLNToAnki
{
    public class WordBuilder
    {
        public Word GetWord(string item)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(item);

            var hTMLInterpreter = new HTMLInterpreter(htmlDocument);
            
            var titleNode = hTMLInterpreter.GetNodeByNameAndAttribute("div", "dc-title\"\"");
            var wordTextNode = hTMLInterpreter.GetNodeByNameAndAttribute("span", "dc-gap\"\"");
            var question = new QuestionBuilder().Build(htmlDocument.DocumentNode);

            return new Word()
            {
                EpisodTitle = titleNode.LastChild.InnerText,
                Text = wordTextNode.LastChild.InnerText
            };
        }
    }
}