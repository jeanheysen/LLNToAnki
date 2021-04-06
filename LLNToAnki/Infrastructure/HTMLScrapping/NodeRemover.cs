using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace LLNToAnki.Infrastructure.HTMLScrapping
{
    public interface INodeRemover
    {
        HtmlNode Remove(HtmlNode doc, string name);
    }

    public class NodeRemover : INodeRemover
    {
        public HtmlNode Remove(HtmlNode doc, string name)
        {

            List<string> xpaths = new List<string>();

            var paths = doc.Descendants("script").Select(n => n.XPath).ToList();

            foreach (string xpath in paths)
            {
                var htmlNode = doc.SelectSingleNode(xpath);

                if (htmlNode == null) continue;

                htmlNode.Remove();
            }
            return doc;
        }
    }

}
