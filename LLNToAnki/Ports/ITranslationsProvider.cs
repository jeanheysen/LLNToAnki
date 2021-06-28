namespace LLNToAnki.Business.Ports
{
    public interface ITranslationDetailer
    {
        IURLBuilder UrlBuilder { get; }

        string GetAll(string word);
    }

    public interface IURLBuilder
    {
        string CreateURL(string word);
    }
}
