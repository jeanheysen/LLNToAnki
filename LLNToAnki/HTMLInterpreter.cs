using HtmlAgilityPack;
using System.Linq;

namespace LLNToAnki
{
    public class HTMLInterpreter
    {
        private readonly HtmlDocument htmlDoc;
        public string Html { get; }

        public HTMLInterpreter(string html)
        {
            htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            Html = html;
        }


        public HtmlNode GetNodeByNameAndAttribute(string name, string attribute)
        {
            var divs = htmlDoc.DocumentNode.Descendants().Where(n => n.Name == name).ToList();

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
    }
}