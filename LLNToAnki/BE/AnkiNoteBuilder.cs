namespace LLNToAnki.BE
{
    public interface IAnkiNoteBuilder
    {
        IAnkiNote Build(IWordItem item);
    }

    public class AnkiNoteBuilder : IAnkiNoteBuilder
    {
        public AnkiNoteBuilder()
        {

        }

        public IAnkiNote Build(IWordItem item)
        {
            var note = new AnkiNote();

            note.Question = item.ContextWithWordColored;

            note.Answer = item.Translation;

            note.Audio = item.Audio;

            note.Source = BuildSource(item.Word);

            return note;
        }

        private string BuildSource(string word)
        {
            return $"<a href=\"https://www.wordreference.com/enfr/{word}\">https://www.wordreference.com/enfr/{word}</a>";
        }
    }
}