using LLNToAnki.BE.Ports;

namespace LLNToAnki.Infrastructure.URLBuilding
{
    public class WordReferenceURLBuilder : IURLBuilder
    {
        public string CreateURL(string word)
        {
            return $"https://www.wordreference.com/enfr/{word}";
        }
    }
}
