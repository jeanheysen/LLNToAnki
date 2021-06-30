using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LLNToAnki.Business.Logic
{
    public interface ISnapshotBL
    {
        IReadOnlyList<Snapshot> Create(string rawLlnOutput);
        
        void UpdateLanguage(Guid id, Guid languageId);
    }

    [System.ComponentModel.Composition.Export(typeof(ISnapshotBL)), System.Composition.Shared]
    public class SnapshotBL : ISnapshotBL
    {
        private readonly IContextProvider contextProvider;
        private readonly ILanguageBL languageBL;

        [System.ComponentModel.Composition.ImportingConstructor]
        public SnapshotBL(IContextProvider contextProvider, ILanguageBL languageBL)
        {
            this.contextProvider = contextProvider;
            this.languageBL = languageBL;
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
                Id = Guid.NewGuid(),
                HtmlContent = subitems[0],
                Audio = subitems[1],
                Tag = subitems[2]
            };
        }

        public void UpdateLanguage(Guid id, Guid languageId)
        {
            var snapshot = contextProvider.Context.Snapshots.First(s => s.Id == id);

            var language = languageBL.GetById(languageId);

            snapshot.Language = language;
        }
    }
}