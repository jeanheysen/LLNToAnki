namespace LLNToAnki.BE.Ports
{
    public interface ITranslationsProvider
    {
        string GetTranslations(string word);
    }
}
