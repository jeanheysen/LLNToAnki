namespace LLNToAnki.BE.Ports
{
    public interface ITranslationDetailsProvider
    {
        string GetTranslations(string word);
    }
}
