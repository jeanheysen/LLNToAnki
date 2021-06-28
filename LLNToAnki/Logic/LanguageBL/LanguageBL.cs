using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LLNToAnki.Business.Logic
{
    public interface ILanguageBL
    {
        List<Language> GetAll();
        Language GetFromAcronymeOrDefault(string acronyme);
    }

    public class LanguageBL : ILanguageBL
    {
        private Language defaultLanguage;

        public LanguageBL()
        {
            defaultLanguage = new Language() { Name = "English" };
        }

        public List<Language> GetAll()
        {
            return new List<Language>()
            {
                new Language(){Name="Dutch"},
                new Language(){Name="English"}
            };
        }

        public Language GetFromAcronymeOrDefault(string acronyme)
        {
            if (acronyme == "en") return new Language() { Name = "English" };
            
            else if (acronyme == "nl") return new Language() { Name = "Dutch" };
            
            else return defaultLanguage;
        }
    }
}
