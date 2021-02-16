namespace LLNToAnki
{
    public class AnkiNoteBuilder
    {
        public AnkiNote Builder(WordItem item)
        {
            var note = new AnkiNote();
            note.Question = item.Context.Question;
            note.Answer = item.Context.Translation;
            note.Before = "";
            return note;
        }
    }
}