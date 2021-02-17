﻿using HtmlAgilityPack;
using System.Collections.Generic;

namespace LLNToAnki
{
    public interface IWordItemBuilder
    {
        IWordItem Build(string html);
    }

    public class WordItemBuilder : IWordItemBuilder
    {
        //FIELDS
        private readonly IHTMLScraper htmlScraper;

        //CONSTRUCTOR
        public WordItemBuilder(IHTMLScraper htmlScraper)
        {
            this.htmlScraper = htmlScraper;
        }


        //METHODS
        public IWordItem Build(string html)
        {
            return new WordItem()
            {
                Word = GetWord(html),
                
                EpisodTitle = GetTitle(html),
                
                ContextWithWordColored = GetContextWithWordColored(html),
                
                Translation = GetTranslation(html)
            };
        }

        private string GetTitle(string html)
        {
            var node = htmlScraper.GetNodeByNameAndAttribute(html, "div", "dc-title\"\"");
            return node.LastChild.InnerText;
        }

        private string GetWord(string html)
        {
            var node = htmlScraper.GetNodeByNameAndAttribute(html, "span", "dc-gap\"\"");
            return node.LastChild.InnerText;
        }

        private string GetContextWithWordColored(string html)
        {
            var translation = GetTranslation(html);

            var a1 = html;
            var a2 = a1.Replace("{{c1::", "");
            var a3 = a2.Replace("}}", "");
            var a4 = a3.Replace(translation, "");

            var toreplace = "<span class=\"\"dc-gap\"\">";
            var by = "<span class=\"\"dc-gap\"\" style=\"\"background-color: brown;\"\">";
            var a5 = a4.Replace(toreplace, by);
            return a5;
        }

        private string GetTranslation(string html)
        {
            var node = htmlScraper.GetNodeByNameAndAttribute(html, "div", "dc-translation");
            return node.LastChild.InnerText;
        }
    }
}