using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;

namespace LLNToAnki.Business.Logic
{
    public interface IWordItemBuilder
    {
        TargetSequence Build(Snapshot llnItem);
    }

    public class WordItemBuilder : IWordItemBuilder
    {
        //FIELDS
        private readonly IDataScraper htmlScraper;

        //CONSTRUCTOR
        public WordItemBuilder(IDataScraper htmlScraper)
        {
            this.htmlScraper = htmlScraper;
        }


        //METHODS
        public TargetSequence Build(Snapshot llnItem)
        {
            var html = llnItem.HtmlContent;

            return new TargetSequence()
            {
                Sequence = GetWord(html),

                EpisodTitle = GetTitle(html),

                ContextWithWordColored = GetContextWithWordColored(html),

                Translation = GetTranslation(html),

                Audio = llnItem.Audio
            };
        }

        private string GetTitle(string html)
        {
            var node = htmlScraper.GetNodeByNameAndAttribute(html, "div", "dc-title\"\"");
            return node.LastChild.InnerText;
        }

        private string GetWord(string html)
        {
            var node = htmlScraper.GetNodeByNameAndAttribute(html, "span", "dc-gap\"\"");
            return node.LastChild.InnerText;
        }

        private string GetContextWithWordColored(string html)
        {
            var translation = GetTranslation(html);

            var a1 = html;
            var a2 = a1.Replace("{{c1::", "");
            var a3 = a2.Replace("}}", "");
            var a4 = a3.Replace(translation, "");

            var toreplace = "<span class=\"\"dc-gap\"\">";
            var by = "<span class=\"\"dc-gap\"\" style=\"\"background-color: brown;\"\">";
            var a5 = a4.Replace(toreplace, by);

            //TODO ce mic mac est à bouger dans le ConnectNoteBuilder
            var a6 = a5.Replace("\"\"", "\"");
            var a7 = a6.Substring(1, a6.Length - 2);//remove the first " and the last "

            var a8 = a7.Replace(".nightMode.card {\n        background: black;\n    }\n    \n    ", "");
            
            a8 = a8.Replace("\n        background-color: white;", "");
            
            //a8 = a8.Replace("\n        background: rgb(255,243,248);", "");
            
            a8 = a8.Replace(".nightMode .dc-card {\n        background: #333;\n        border-bottom: none;\n    }", "");
            
            a8 = a8.Replace(".card {\n        background: rgb(255,243,248);\n        background: linear-gradient(76deg, rgba(255,243,248,1) 0%, rgba(238,246,255,1) 100%);\n    }", "");
            
            return a8;
        }

        private string GetTranslation(string html)
        {
            var node = htmlScraper.GetNodeByNameAndAttribute(html, "div", "dc-translation");
            return node.LastChild.InnerText;
        }
    }
}