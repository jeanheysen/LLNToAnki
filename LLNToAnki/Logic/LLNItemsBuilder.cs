using LLNToAnki.Domain;
using System.Collections.Generic;
using System.Linq;

namespace LLNToAnki.Business.Logic
{
    public interface ILLNItemsBuilder
    {
        IReadOnlyList<LLNItem> Build(string rawLlnOutput);
    }

    public class LLNItemsBuilder : ILLNItemsBuilder
    {
        public LLNItemsBuilder()
        {
        }

        public IReadOnlyList<LLNItem> Build(string rawLlnOutput)
        {
            var separator = "\"<style>";

            var all = rawLlnOutput.Split(separator).ToList();

            var r = new List<LLNItem>();
            int counter = 0;

            foreach (var item in all)
            {
                if (counter++ == 0) continue;

                var content = string.Concat(separator, item);

                r.Add(CreateItemForRawCut(content));
            }

            return r;
        }

        private LLNItem CreateItemForRawCut(string content)
        {
            var subitems = content.Split("\t"); //todo use splitter
            return new LLNItem()
            {
                HtmlContent = subitems[0],
                Audio = subitems[1],
                Tag = subitems[2]
            };
        }
    }
}