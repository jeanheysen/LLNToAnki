using LLNToAnki.BE.Ports;
using LLNToAnki.Infrastructure.URLBuilding;
using System.Collections.Generic;

namespace LLNToAnki.Infrastructure.HTMLScrapping
{
    public class MijnWordenboekDetailer : ITranslationDetailer
    {
        public IURLBuilder UrlBuilder { get; }
        private htmlReader htmlReader;
        private IDataScraper scraper;


        public MijnWordenboekDetailer(IURLBuilder urlBuilder, IDataScraper scraper, htmlReader htmlReader)
        {
            this.UrlBuilder = urlBuilder;

            this.scraper = scraper;

            this.htmlReader = htmlReader;
        }


        public string GetAll(string word)
        {
            var url = UrlBuilder.CreateURL(word);

            var mainNode = htmlReader.GetHTML(url);

            var node = scraper.GetNodeByNameAndAttribute(mainNode, "div", "class", "slider-wrap");

            var endNode = new NodeRemover().Remove(node, "script");

            return CleanFromSyntaxExplanations(endNode.ParentNode.InnerHtml);
        }

        private string CleanFromSyntaxExplanations(string content)
        {
            var toRemove = new List<string>();

            //toRemove.Add("");

            foreach (var s in toRemove)
            {
                if (content.Contains(s)) content = content.Replace(s, "");
            }
            return content;
        }
    }

}
