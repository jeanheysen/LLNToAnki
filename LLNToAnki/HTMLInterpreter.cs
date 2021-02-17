using HtmlAgilityPack;
using System.Linq;

namespace LLNToAnki
{
    public interface IHTMLInterpreter
    {
        HtmlNode GetNodeByNameAndAttribute(string html, string name, string attribute);
    }

    public class HTMLInterpreter : IHTMLInterpreter
    {
        public HTMLInterpreter()
        {
        }


        public HtmlNode GetNodeByNameAndAttribute(string html, string name, string attribute)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

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