using LLNToAnki.Infrastructure.URLBuilding;

namespace LLNToAnki.BE.Ports
{
    public interface ITranslationDetailer
    {
        IURLBuilder UrlBuilder { get; }

        string GetAll(string word);
    }
}
