namespace LLNToAnki
{
    public interface IAnkiNote
    {
        string AddReverseAnswer { get; set; }
        string AddReverseQuestion { get; set; }
        string After { get; set; }
        string Answer { get; set; }
        string Audio { get; set; }
        string Before { get; set; }
        string Mem_Image { get; set; }
        string Mem_Text { get; set; }
        string Question { get; set; }
        string Source { get; set; }
    }

    public class AnkiNote : IAnkiNote
    {
        public string Question { get; set; }
        public string Before { get; set; }
        public string Answer { get; set; }
        public string After { get; set; }
        public string Source { get; set; }
        public string Audio { get; set; }
        public string Mem_Image { get; set; }
        public string Mem_Text { get; set; }
        public string AddReverseQuestion { get; set; }
        public string AddReverseAnswer { get; set; }
    }
}