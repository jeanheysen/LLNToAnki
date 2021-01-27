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

            var context = new WordContext();
            context.EpisodTitle = extractor.GetTitle();

            return new WordItem()
            {
                Word = extractor.GetWord(),
                Context = context
            };
        }
    }
}