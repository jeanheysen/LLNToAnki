using HtmlAgilityPack;

namespace LLNToAnki
{
    public class AnkiNoteBuilder
    {
        public AnkiNoteBuilder()
        {

        }

        public AnkiNote Build(HtmlNode node)
        {
            var questionBuilder = new QuestionBuilder();

            return new AnkiNote()
            {
                Question = questionBuilder.Build(node)
            };
        }
    }
}