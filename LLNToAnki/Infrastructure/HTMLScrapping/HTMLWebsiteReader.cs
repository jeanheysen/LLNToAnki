using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LLNToAnki.Infrastructure
{
    public interface htmlReader
    {
        void DirectDownload(string url, string localPath);
        HtmlNode GetHTML(string path);
    }

    public class HTMLWebsiteReader : htmlReader
    {
        public HtmlNode GetHTML(string path)
        {
            var web = new HtmlWeb();
            HtmlNode node;
            try
            {
                node = web.Load(path).DocumentNode;
            }
            catch (Exception)
            {
                throw new Exception($"Failed while downloading translation from {path}");
            }

            return node;
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
