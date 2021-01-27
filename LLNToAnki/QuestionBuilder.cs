using HtmlAgilityPack;

namespace LLNToAnki
{
    public class QuestionBuilder
    {
        public string Build(HtmlNode htmlContent)
        {
            HtmlNode nodeToRemove = null;
            foreach (var node in htmlContent.Descendants())
            {
                foreach (var attribute in node.Attributes)
                {
                    if (attribute.Name.Contains("dc-translation"))
                    {
                        nodeToRemove = node;
                    }
                }
            }
            nodeToRemove?.Remove();

            return htmlContent.OuterHtml;
        }
    }
}
