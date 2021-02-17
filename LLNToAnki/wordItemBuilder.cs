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
            return new WordItem() 
            {
                Word = extractor.GetWord(html),
                EpisodTitle = extractor.GetTitle(html),
                Question = extractor.GetQuestion(html),
                Translation = extractor.GetTranslation(html)
            };
        }
    }
}