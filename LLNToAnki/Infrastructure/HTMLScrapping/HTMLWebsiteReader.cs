using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LLNToAnki.Infrastructure
{
    public interface IHTMLWebsiteReader
    {
        void DirectDownload(string url, string localPath);
        HtmlNode GetHTML(string path);
    }

    public class HTMLWebsiteReader : IHTMLWebsiteReader
    {
        public HtmlNode GetHTML(string path)
        {
            var web = new HtmlWeb();

            return web.Load(path).DocumentNode;
        }

        [Obsolete("use with caution, crashes on WR for unknown reasons.")]
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
