using HtmlAgilityPack;
using System.Collections.Generic;

namespace LLNToAnki
{
    public class WordItemBuilder
    {
        public WordItemBuilder()
        {

        }

        public WordItem Build(string html)
        {
            var hTMLInterpreter = new HTMLInterpreter();

            var extractor = new WordItemExtractor(hTMLInterpreter);

            var word = extractor.GetWord(html);

            var title = extractor.GetTitle(html);

            var question = extractor.GetQuestion(html);

            var context = new WordContext
            {
                EpisodTitle = title,
                Question = question,
                Translation = extractor.GetTranslation(html)
            };

            return new WordItem() { Word = word, Context = context };
        }
    }
}