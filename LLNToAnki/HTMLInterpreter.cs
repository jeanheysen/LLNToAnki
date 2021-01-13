using HtmlAgilityPack;
using System.Linq;

namespace LLNToAnki
{
    public class HTMLInterpreter
    {
        public HtmlNode GetTitle(HtmlDocument htmlDoc)
        {
            var divs = htmlDoc.DocumentNode.Descendants().Where(n => n.Name == "div").ToList();

            foreach (var div in divs)
            {
                foreach (var att in div.Attributes)
                {
                    if (att.Name == "dc-title\"\"")
                    {
                        return div;
                    }
                }
            }
            return null;
        }
    }
}