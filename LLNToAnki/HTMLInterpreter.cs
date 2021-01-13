using HtmlAgilityPack;
using System.Linq;

namespace LLNToAnki
{
    public class HTMLInterpreter
    {
        private readonly HtmlDocument htmlDoc;

        public HTMLInterpreter(HtmlDocument htmlDoc)
        {
            this.htmlDoc = htmlDoc;
        }

        public HtmlNode GetNodeByNameAndAttribute(string name, string attribute)
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