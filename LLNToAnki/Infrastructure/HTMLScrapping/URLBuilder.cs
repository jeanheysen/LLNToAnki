namespace LLNToAnki.Infrastructure.HTMLScrapping
{
    public interface IURLBuilder
    {
        string OnlineWordReference(string word);
    }

    public class URLBuilder : IURLBuilder
    {
        public string OnlineWordReference(string word)
        {
            return $"https://www.wordreference.com/enfr/{word}";
        }
    }
}
