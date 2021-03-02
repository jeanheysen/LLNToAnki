using LLNToAnki.BE.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace LLNToAnki.Infrastructure.HTMLScrapping
{
    public class WordReferenceTranslationsProvider : ITranslationsProvider
    {
        private HTMLWebsiteReader websiteReader;
        private HTMLScraper scraper;

        public WordReferenceTranslationsProvider()
        {
            websiteReader = new HTMLWebsiteReader();

            scraper = new HTMLScraper();
        }

        public string GetTranslations(string word)
        {
            var url = $"https://www.wordreference.com/enfr/{word}";

            var mainNode = websiteReader.GetHTML(url);

            var node = scraper.GetNodeByNameAndAttribute(mainNode, "table", "class");

            return node.ParentNode.InnerHtml;
        }
    }
}
