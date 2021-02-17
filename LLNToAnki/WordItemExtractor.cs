namespace LLNToAnki
{
    public class WordItemExtractor
    {
        private readonly IHTMLInterpreter interpreter;

        public WordItemExtractor(IHTMLInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public string GetTitle(string html)
        {
            var node = interpreter.GetNodeByNameAndAttribute(html, "div", "dc-title\"\"");
            return node.LastChild.InnerText;
        }

        public string GetWord(string html)
        {
            var node = interpreter.GetNodeByNameAndAttribute(html, "span", "dc-gap\"\"");
            return node.LastChild.InnerText;
        }

        public string GetQuestion(string html)
        {
            var translation = GetTranslation(html);

            var a1 = html;
            var a2 = a1.Replace("{{c1::", "");
            var a3 = a2.Replace("}}", "");
            var a4 = a3.Replace(translation, "");

            var toreplace = "<span class=\"\"dc-gap\"\">";
            var by = "<span class=\"\"dc-gap\"\" style=\"\"background-color: brown;\"\">";
            var a5 = a4.Replace(toreplace, by);
            return a5;
        }

        public string GetTranslation(string html)
        {
            var node = interpreter.GetNodeByNameAndAttribute(html, "div", "dc-translation");
            return node.LastChild.InnerText;
        }
    }
}