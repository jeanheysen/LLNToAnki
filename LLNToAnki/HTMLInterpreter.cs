using HtmlAgilityPack;
using System.Linq;

namespace LLNToAnki
{
    public class HTMLInterpreter
    {
        private readonly HtmlDocument htmlDoc;

        public HTMLInterpreter(string html)
        {
            htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

        }

        public HTMLInterpreter(HtmlDocument htmlDoc)
        {
            this.htmlDoc = htmlDoc;
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

    public class WordItemExtractor
    {
        private readonly HTMLInterpreter interpreter;

        public WordItemExtractor(HTMLInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public string GetTitle()
        {
            var node = interpreter.GetNodeByNameAndAttribute("div", "dc-title\"\"");
            return node.LastChild.InnerText;
        }

        public string GetWord()
        {
            var node = interpreter.GetNodeByNameAndAttribute("span", "dc-gap\"\"");
            return node.LastChild.InnerText;
        }

        public string GetTranslation()
        {
            var node = interpreter.GetNodeByNameAndAttribute("div", "dc-translation");
            return node.LastChild.InnerText;
        }
    }
}