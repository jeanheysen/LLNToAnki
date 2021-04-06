namespace LLNToAnki.Infrastructure.URL
{
    public class WordReferenceURLBuilder : IURLBuilder
    {
        public string CreateURL(string word)
        {
            return $"https://www.wordreference.com/enfr/{word}";
        }
    }
}
