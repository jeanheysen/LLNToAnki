namespace LLNToAnki.BE
{
    public interface IWordItem
    {
        string EpisodTitle { get; set; }
        string ContextWithWordColored { get; set; }
        string Translation { get; set; }
        string Word { get; set; }
    }

    public class WordItem : IWordItem
    {
        public string Word { get; set; }

        public string EpisodTitle { get; set; }

        public string ContextWithWordColored { get; set; }

        public string Translation { get; set; }
    }
}
