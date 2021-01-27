namespace LLNToAnki
{
    public class WordItem
    {
        public string Word { get; set; }
        
        public WordContext Context { get; set; }
    }

    public class WordContext
    {
        public string EpisodTitle { get; set; }
    }
}
