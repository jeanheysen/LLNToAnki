namespace LLNToAnki.BE
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

            note.Question = item.ContextWithWordColored;

            note.Answer = item.Translation;

            note.Audio = item.Audio;

            return note;
        }
    }
}