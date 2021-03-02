using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
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

        public void DirectDownload(string url, string localPath)
        {
            WebClient client = new WebClient();

            try
            {
                client.DownloadFile(url, localPath);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
