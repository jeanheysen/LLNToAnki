using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
using LLNToAnki.Infrastructure.URLBuilding;

namespace LLNToAnki.Infrastructure.HTMLScrapping
{
    [System.ComponentModel.Composition.Export(typeof(IDictionaryAbstractFactory)), System.Composition.Shared]
    public class DictionaryAbstractFactory : IDictionaryAbstractFactory
    {
        private readonly IDataScraper scraper;
        private readonly IHtmlReader websiteReader;

        [System.ComponentModel.Composition.ImportingConstructor]
        public DictionaryAbstractFactory(IDataScraper scraper, IHtmlReader websiteReader)
        {
            this.scraper = scraper;
            this.websiteReader = websiteReader;
        }

        public ITranslationDetailer Provide(Language language)
        {
            if (language.Name == "English")
            {
                return new WordReferenceDetailer(new WordReferenceURLBuilder(), scraper, websiteReader);
            }

            else if (language.Name == "Dutch")
            {
                return new MijnWordenboekDetailer(new MijnWordenboekURLBuilder(), scraper, websiteReader);
            }
            else throw new System.Exception($"No detailer for the language {language}");

        }
    }
}

