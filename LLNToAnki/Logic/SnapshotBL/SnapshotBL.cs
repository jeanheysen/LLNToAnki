using LLNToAnki.Domain;
using System.Collections.Generic;
using System.Linq;

namespace LLNToAnki.Business.Logic
{
    public interface ISnapshotBL
    {
        IReadOnlyList<Snapshot> Create(string rawLlnOutput);
    }

    [System.ComponentModel.Composition.Export(typeof(ISnapshotBL)), System.Composition.Shared]
    public class SnapshotBL : ISnapshotBL
    {
        public SnapshotBL()
        {
        }

        public IReadOnlyList<Snapshot> Create(string rawLlnOutput)
        {
            var separator = "\"<style>";

            var all = rawLlnOutput.Split(separator).ToList();

            var r = new List<Snapshot>();
            int counter = 0;

            foreach (var item in all)
            {
                if (counter++ == 0) continue;

                var content = string.Concat(separator, item);

                r.Add(CreateItemForRawCut(content));
            }

            return r;
        }

        private Snapshot CreateItemForRawCut(string content)
        {
            var subitems = content.Split("\t"); //todo use splitter
            return new Snapshot()
            {
                HtmlContent = subitems[0],
                Audio = subitems[1],
                Tag = subitems[2]
            };
        }
    }
}