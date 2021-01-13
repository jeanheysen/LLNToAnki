namespace LLNToAnki
{
    public class QuestionBuilder
    {
        public string Build(HtmlAgilityPack.HtmlNode htmlContent)
        {
            return htmlContent.OuterHtml;
        }
    }
}
