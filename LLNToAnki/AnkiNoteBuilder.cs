namespace LLNToAnki
{
    public class AnkiNoteBuilder
    {
        public AnkiNote Builder(WordItem item)
        {
            var note = new AnkiNote();
            note.Question = item.Context.HtmlWithWordInIt;
            note.Answer = item.Word;
            note.Before = "";
            return note;
        }
    }
}