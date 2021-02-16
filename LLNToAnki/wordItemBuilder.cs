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

            var question = extractor.GetQuestion();

            var context = new WordContext
            {
                EpisodTitle = title,
                Question = question,
                Translation = extractor.GetTranslation()
            };

            return new WordItem() { Word = word, Context = context };
        }
    }
}