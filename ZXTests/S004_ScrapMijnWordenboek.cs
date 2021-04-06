using LLNToAnki.Infrastructure;
using NUnit.Framework;

namespace ZXTests
{
    public class S004_ScrapMijnWordenboek : BaseIntegrationTesting
    {
        private HTMLWebsiteReader htmlreader;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            htmlreader = new HTMLWebsiteReader();
        }

       
    }
}
