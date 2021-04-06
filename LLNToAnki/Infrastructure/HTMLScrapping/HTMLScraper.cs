using HtmlAgilityPack;
using LLNToAnki.BE.Ports;
using System.Linq;

namespace LLNToAnki.Infrastructure
{

    public class HTMLScraper : IDataScraper
    {
        public HtmlNode GetNodeByNameAndAttribute(HtmlNode htmlNode, string name, string attribute)
        {
            var divs = htmlNode.Descendants().Where(n => n.Name == name).ToList();

            foreach (var div in divs)
            {
                foreach (var att in div.Attributes)
                {
                    if (att.Name == attribute)
                    {
                        return div;
                    }
                }
            }
            return null;
        }

        public HtmlNode GetNodeByNameAndAttribute(string html, string name, string attribute)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var htmlNode = htmlDoc.DocumentNode;

            return GetNodeByNameAndAttribute(htmlNode, name, attribute);
        }
    }
}