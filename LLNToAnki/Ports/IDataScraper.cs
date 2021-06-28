using HtmlAgilityPack;

namespace LLNToAnki.Business.Ports
{
    public interface IDataScraper
    {
        HtmlNode GetNodeByNameAndAttribute(string html, string name, string attribute);
        HtmlNode GetNodeByNameAndAttribute(HtmlNode htmlNode, string name, string attribute, string value);
    }
}