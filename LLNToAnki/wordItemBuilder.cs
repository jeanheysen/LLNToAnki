using HtmlAgilityPack;
using System.Collections.Generic;

namespace LLNToAnki
{
    public class wordItemBuilder
    {
        public WordItem Build(string html)
        {
            var hTMLInterpreter = new HTMLInterpreter(html);

            var extractor = new WordItemExtractor(hTMLInterpreter);
            var word = extractor.GetWord();
            var title = extractor.GetTitle();
            var htmlContent = html;


            var context = new WordContext { EpisodTitle = title,HtmlWithWordInIt=htmlContent };
            return new WordItem() { Word = word, Context = context };
        }
    }
}