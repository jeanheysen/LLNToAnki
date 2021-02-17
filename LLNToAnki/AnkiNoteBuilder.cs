namespace LLNToAnki
{
    public class AnkiNoteBuilder
    {
        public AnkiNote Builder(WordItem item)
        {
            var note = new AnkiNote();
            
            note.Question = item.Question;
            
            note.Answer = item.Translation;
            
            return note;
        }
    }
}