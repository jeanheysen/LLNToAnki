using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
using System.Text;

namespace LLNToAnki.Business.Logic
{
    public interface IAnkiNoteBuilder
    {
        AnkiNote Create(TargetSequence item);
    }

    public class AnkiNoteBuilder : IAnkiNoteBuilder
    {
        private readonly ITranslationDetailer translationsProvider;

        public AnkiNoteBuilder(ITranslationDetailer translationsProvider)
        {
            this.translationsProvider = translationsProvider;
        }

        public AnkiNote Create(TargetSequence item)
        {
            //todo devrait etre séparé en deux méthode : Post et Get
            var note = new AnkiNote();

            note.Question = item.ContextWithWordColored;

            note.Answer = item.Translation;

            note.Audio = item.Audio;

            note.Source = BuildSource(item.Sequence);

            note.After = BuildAfter(item.Translation, item.Sequence);

            return note;
        }

        private string BuildSource(string word)
        {
            var url = translationsProvider.UrlBuilder.CreateURL(word);

            return $"<a href=\"{url}\">{url}</a>";
        }

        private string BuildAfter(string sentence, string word)
        {
            var sb = new StringBuilder();

            sb.Append($"Traduction Netflix : \"{sentence}\".");

            var translations = translationsProvider.GetAll(word);

            sb.Append(translations);

            return sb.ToString();
        }
    }

}