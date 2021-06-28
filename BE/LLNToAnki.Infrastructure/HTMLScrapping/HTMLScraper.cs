using HtmlAgilityPack;
using LLNToAnki.BE.Ports;
using System.Collections.Generic;
using System.Linq;

namespace LLNToAnki.Infrastructure.HTMLScrapping
{
    public class HTMLScraper : IDataScraper
    {
        public HtmlNode GetNodeByNameAndAttribute(HtmlNode htmlNode, string name, string attribute, string value)
        {
            var allWithName = htmlNode.Descendants(name).ToList();
            var l = new List<HtmlNode>();

            foreach (var div in allWithName)
            {
                foreach (var att in div.Attributes)
                {
                    if (att.Name == attribute)
                    {
                        if (string.IsNullOrEmpty(value) || value.Equals(att.Value))
                        {
                            l.Add(div);
                        }
                    }
                }
            }

            return l.Last();
        }

        public HtmlNode GetNodeByNameAndAttribute(string html, string name, string attribute)
        {
            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(html);

            var htmlNode = htmlDoc.DocumentNode;

            return GetNodeByNameAndAttribute(htmlNode, name, attribute, null);
        }
    }
}