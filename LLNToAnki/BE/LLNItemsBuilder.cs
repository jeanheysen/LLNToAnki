using LLNToAnki.BE.Ports;
using System.Collections.Generic;

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
            var all = textSplitter.SplitOnTab(rawLlnOutput);

            int counter = 0;

            var r = new List<LLNItem>();

            foreach (var item in all)
            {
                if (counter == 0) r.Add(new LLNItem() { Content = item });
                if (counter == 2) r.Add(new LLNItem() { Content = item });
                counter++;
            }

            return r;
        }
    }
}