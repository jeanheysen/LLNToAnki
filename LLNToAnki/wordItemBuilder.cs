using HtmlAgilityPack;
using System.Collections.Generic;

namespace LLNToAnki
{
    public class WordItemBuilder
    {
        private readonly IWordItemExtractor extractor;

        public WordItemBuilder(IWordItemExtractor wordItemExtractor)
        {
            this.extractor = wordItemExtractor;
        }

        public WordItem Build(string html)
        {
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