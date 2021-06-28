using LLNToAnki.BE.Ports;
using LLNToAnki.Domain;
using System.Text;

namespace LLNToAnki.BE
{
    public interface IAnkiNoteBuilder
    {
        IAnkiNote Build(IWordItem item);
    }

    public class AnkiNoteBuilder : IAnkiNoteBuilder
    {
        private readonly ITranslationDetailer translationsProvider;

        public AnkiNoteBuilder(ITranslationDetailer translationsProvider)
        {
            this.translationsProvider = translationsProvider;
        }

        public IAnkiNote Build(IWordItem item)
        {
            var note = new AnkiNote();

            note.Question = item.ContextWithWordColored;

            note.Answer = item.Translation;

            note.Audio = item.Audio;

            note.Source = BuildSource(item.Word);

            note.After = BuildAfter(item.Translation, item.Word) ;

            return note;
        }

        private string BuildSource(string word)
        {
            var url = translationsProvider.UrlBuilder.CreateURL(word);
            
            return $"<a href=\"{url}\">{url}</a>";
        }

        private string BuildAfter(string sentence,string word)
        {
            var sb = new StringBuilder();

            sb.Append($"Traduction Netflix : \"{sentence}\".");

            var translations = translationsProvider.GetAll(word);

            sb.Append(translations); 

            return sb.ToString();
        }
    }
}