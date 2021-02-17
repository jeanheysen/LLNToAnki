using HtmlAgilityPack;

namespace LLNToAnki.BE
{
    public interface IHTMLScraper
    {
        HtmlNode GetNodeByNameAndAttribute(string html, string name, string attribute);
    }
}