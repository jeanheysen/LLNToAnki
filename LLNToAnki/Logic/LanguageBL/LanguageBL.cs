using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLNToAnki.Business.Logic
{
    public interface ILanguageBL
    {
        List<Language> GetAll();
        
        Language GetById(Guid languageId);
    }

    [System.ComponentModel.Composition.Export(typeof(ILanguageBL)), System.Composition.Shared]
    public class LanguageBL : ILanguageBL
    {
        private readonly IContextProvider contextProvider;

        [System.ComponentModel.Composition.ImportingConstructor]
        public LanguageBL(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public List<Language> GetAll()
        {
            return contextProvider.Context.Languages;
        }

        public Language GetById(Guid id)
        {
            return contextProvider.Context.Languages.First(l => l.Id == id);
        }
    }
}
