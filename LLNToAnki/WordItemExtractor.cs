namespace LLNToAnki
{
    public class WordItemExtractor
    {
        private readonly HTMLInterpreter interpreter;

        public WordItemExtractor(HTMLInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public string GetTitle()
        {
            var node = interpreter.GetNodeByNameAndAttribute("div", "dc-title\"\"");
            return node.LastChild.InnerText;
        }

        public string GetWord()
        {
            var node = interpreter.GetNodeByNameAndAttribute("span", "dc-gap\"\"");
            return node.LastChild.InnerText;
        }

        public string GetQuestion()
        {
            var a1 = interpreter.Html;
            var a2 = a1.Replace("{{c1::", "");
            var a3 = a2.Replace("}}", "");
            return a3;
        }

        public string GetTranslation()
        {
            var node = interpreter.GetNodeByNameAndAttribute("div", "dc-translation");
            return node.LastChild.InnerText;
        }
    }
}