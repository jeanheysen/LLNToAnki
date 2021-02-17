namespace LLNToAnki
{
    public interface IAnkiNoteBuilder
    {
        IAnkiNote Builder(IWordItem item);
    }

    public class AnkiNoteBuilder : IAnkiNoteBuilder
    {
        public AnkiNoteBuilder()
        {

        }

        public IAnkiNote Builder(IWordItem item)
        {
            var note = new AnkiNote();

            note.Question = item.Question;

            note.Answer = item.Translation;

            return note;
        }
    }
}