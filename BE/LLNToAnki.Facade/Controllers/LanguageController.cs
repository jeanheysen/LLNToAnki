using AutoMapper;
using LLNToAnki.Business;
using LLNToAnki.Business.Logic;
using LLNToAnki.Facade.Dto;
using System.Collections.Generic;

namespace LLNToAnki.Facade.Controllers
{
    public interface ILanguageController
    {
        List<LanguageDto> GetAll();
    }

    [System.ComponentModel.Composition.Export(typeof(ILanguageController)), System.Composition.Shared]
    public class LanguageController : ILanguageController
    {
        private readonly ILanguageBL languageBL;
        private IMapper mapper;

        public LanguageController()
        {
            languageBL = Mef.Container.GetExportedValue<ILanguageBL>(); //todo dans le constructeur

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile)));

            mapper = configuration.CreateMapper();
        }

        public List<LanguageDto> GetAll()
        {
            var all = languageBL.GetAll();

            return mapper.Map<List<LanguageDto>>(all);
        }
    }
}
