namespace LLNToAnki.Domain
{
    public interface ILLNItem
    {
        string Audio { get; set; }
        string HtmlContent { get; set; }
        string Tag { get; set; }
    }

    public class LLNItem : ILLNItem
    {
        public string HtmlContent { get; set; }
        public string Audio { get; set; }
        public string Tag { get; set; }
    }
}