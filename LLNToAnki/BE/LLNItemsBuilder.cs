using LLNToAnki.BE.Ports;
using System.Collections.Generic;
using System.Linq;

namespace LLNToAnki.BE
{
    public interface ILLNItemsBuilder
    {
        IReadOnlyList<ILLNItem> Build(string rawLlnOutput);
    }

    public class LLNItemsBuilder : ILLNItemsBuilder
    {
        public LLNItemsBuilder()
        {
        }

        public IReadOnlyList<ILLNItem> Build(string rawLlnOutput)
        {
            var separator = "\"<style>";

            var all = rawLlnOutput.Split(separator).ToList();

            var r = new List<ILLNItem>();
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