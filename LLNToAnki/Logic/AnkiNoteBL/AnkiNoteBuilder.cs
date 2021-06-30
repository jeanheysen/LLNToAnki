using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
using System.Text;

namespace LLNToAnki.Business.Logic
{
    public interface IAnkiNoteBuilder
    {
        AnkiNote Create(TargetSequence item);
    }

    [System.ComponentModel.Composition.Export(typeof(IAnkiNoteBuilder)), System.Composition.Shared]
    public class AnkiNoteBuilder : IAnkiNoteBuilder
    {
        private readonly IDictionaryAbstractFactory dictionaryAbstractFactory;

        [System.ComponentModel.Composition.ImportingConstructor]
        public AnkiNoteBuilder(IDictionaryAbstractFactory dictionaryAbstractFactory)
        {
            this.dictionaryAbstractFactory = dictionaryAbstractFactory;
        }

        public AnkiNote Create(TargetSequence item)
        {
            //todo devrait etre séparé en deux méthode : Post et Get
            var note = new AnkiNote();

            note.Question = item.ContextWithWordColored;

            note.Answer = item.Translation;

            note.Audio = item.Audio;

            var translationsProvider = dictionaryAbstractFactory.Provide(item.SnapShot.Language);

            note.Source = BuildSource(item.Sequence, translationsProvider);

            note.After = BuildAfter(item.Translation, item.Sequence, translationsProvider);

            return note;
        }

        private string BuildSource(string word, ITranslationDetailer detailer)
        {
            var url = detailer.UrlBuilder.CreateURL(word);

            return $"<a href=\"{url}\">{url}</a>";
        }

        private string BuildAfter(string sentence, string word, ITranslationDetailer detailer)
        {
            var sb = new StringBuilder();

            sb.Append($"Traduction Netflix : \"{sentence}\".");

            var translations = detailer.GetAll(word);

            sb.Append(translations);

            return sb.ToString();
        }
    }

}