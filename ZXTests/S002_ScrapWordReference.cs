using HtmlAgilityPack;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ZXTests
{
    public class S002_ScrapWordReference
    {
        string wordReferenceURL = @"https://www.wordreference.com/enfr/eyeballs";
        string localWRUrl = @"C:\Users\felix\source\repos\RogerHanain\PXApi\TestPXEngine\Data\WordReferencePages\bread.html";
        string urlNL = @"https://www.mijnwoordenboek.nl/vertaal/NL/FR/brood";

        [Test]
        public async Task T001_ExtractWordReferenceTranslation()
        {
            //Arrange

            //Act
            //var node = await GetFromWeb(wordReferenceURL);
            //var node = await GetFromWeb(urlNL);
            var node = await GetFromWeb(localWRUrl);

            //Act
            Assert.IsNotNull(node);

            //var web = new HtmlAgilityPack.
            //
            ////Act
            //web.Load(wordReferenceURL);
            //var node = document.DocumentNode;
            //var scrapper = new HTMLScraper();
            //scrapper.GetNodeByNameAndAttribute(node, "table", "class=\"WRD\"");
        }

        private async Task<HtmlNode> GetFromWeb(string url)
        {
            // instance or static variable
            var client = new HttpClient();


            // get answer in non-blocking way
            using (var response = await client.GetAsync(url))
            {
                using (var content = response.Content)
                {
                    // read answer in non-blocking way
                    var result = await content.ReadAsStringAsync();

                    var document = new HtmlDocument();

                    document.LoadHtml(result);

                    return document.DocumentNode;
                    //var nodes = document.DocumentNode.SelectNodes("Your nodes");
                    //Some work with page....
                }
            }
        }

        [Test]
        public void T002_SaveHtmLocally()
        {
            WebClient client = new WebClient();
            string downloadString = client.DownloadString(wordReferenceURL);
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            //System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            //HtmlTextWriter oHtmlTextWriter = new
            //System.Web.UI.HtmlTextWriter(oStringWriter);
            //Page.RenderControl(oHtmlTextWriter);
            //oHtmlTextWriter.Flush();
            //System.IO.FileStream fs = new
            //System.IO.FileStream(@"C:\temp\test.htm", System.IO.FileMode.Create);
            //string s = oStringWriter.ToString();
            //byte[] b = System.Text.Encoding.UTF8.GetBytes(s);
            //fs.Write(b, 0, b.Length);
            //fs.Close();
            //Response.End();
        }
    }
}
