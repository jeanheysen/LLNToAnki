using LLNToAnki.BE.Ports;
using LLNToAnki.Infrastructure.URLBuilding;
using System.Collections.Generic;

namespace LLNToAnki.Infrastructure.HTMLScrapping
{
    public class MijnWordenboekDetailer : ITranslationDetailer
    {
        private readonly IURLBuilder urlBuilder;
        private IHTMLWebsiteReader websiteReader;
        private IDataScraper scraper;


        public MijnWordenboekDetailer(IURLBuilder urlBuilder, IDataScraper scraper, IHTMLWebsiteReader websiteReader)
        {
            this.urlBuilder = urlBuilder;

            this.scraper = scraper;

            this.websiteReader = websiteReader;
        }


        public string GetAll(string word)
        {
            var url = urlBuilder.CreateURL(word);

            var mainNode = websiteReader.GetHTML(url);

            var node = scraper.GetNodeByNameAndAttribute(mainNode, "div", "class", "slider-wrap");

            return CleanFromSyntaxExplanations(node.ParentNode.InnerHtml);
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
