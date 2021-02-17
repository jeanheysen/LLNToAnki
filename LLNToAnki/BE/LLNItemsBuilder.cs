using LLNToAnki.BE.Ports;
using System.Collections.Generic;
using System.Linq;

namespace LLNToAnki.BE
{
    public class LLNItemsBuilder
    {
        private readonly ITextSplitter textSplitter;

        public LLNItemsBuilder(ITextSplitter textSplitter)
        {
            this.textSplitter = textSplitter;
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
            var subitems = content.Split("\t");
            return new LLNItem() 
            {
                HtmlContent = subitems[0],
                Audio = subitems[1],
                Tag = subitems[2]
            };
        }
    }
}