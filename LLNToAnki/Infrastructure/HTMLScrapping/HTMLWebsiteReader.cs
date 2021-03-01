using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace LLNToAnki.Infrastructure
{
    public class HTMLWebsiteReader
    {
        public HTMLWebsiteReader()
        {

        }

        public HtmlNode GetHTMLFromLocalPage(string path)
        {
            var web = new HtmlWeb();

            return web.Load(path).DocumentNode;
        }
    }
}
