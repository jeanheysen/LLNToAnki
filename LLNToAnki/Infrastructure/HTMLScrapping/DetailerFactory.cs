using LLNToAnki.BE.Ports;
using LLNToAnki.Domain;
using LLNToAnki.Infrastructure.URLBuilding;

namespace LLNToAnki.Infrastructure.HTMLScrapping
{
    public class DetailerFactory
    {
        private readonly IUrlLAbstractFactory uRLAbstractFactory;
        private readonly IDataScraper scraper;
        private readonly htmlReader websiteReader;

        public DetailerFactory(IUrlLAbstractFactory uRLAbstractFactory, IDataScraper scraper, htmlReader websiteReader)
        {
            this.uRLAbstractFactory = uRLAbstractFactory;
            this.scraper = scraper;
            this.websiteReader = websiteReader;
        }

        public ITranslationDetailer Provide(Language language)
        {
            switch (language)
            {
                case Language.English: return new WordReferenceDetailer(uRLAbstractFactory.CreateUrlBuilder(language),scraper, websiteReader);
                case Language.Dutch: return new   MijnWordenboekDetailer(uRLAbstractFactory.CreateUrlBuilder(language), scraper, websiteReader);
                
                default: throw new System.Exception($"No detailer for the language {language}");
                    
            }
        }
    }
}
