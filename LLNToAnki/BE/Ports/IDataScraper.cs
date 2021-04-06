using HtmlAgilityPack;

namespace LLNToAnki.BE.Ports
{
    public interface IDataScraper
    {
        HtmlNode GetNodeByNameAndAttribute(string html, string name, string attribute);
        HtmlNode GetNodeByNameAndAttribute(HtmlNode htmlNode, string name, string attribute);
    }
}