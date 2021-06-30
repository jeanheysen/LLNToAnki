using AutoMapper;
using LLNToAnki.Business;
using LLNToAnki.Facade.Controllers;
using LLNToAnki.Facade.Dto;
using LLNToAnki.WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LLNToAnki.WPF.Client
{
    public interface IFacadeClient
    {
        Guid Flow_Create(string path);
        
        FlowModel Flow_Get(Guid id);
        
        List<LanguageDto> Language_GetAll();
        
        void Snapshot_UpdateLanguage(Guid id, Guid languageId);
        
        Task TargetSequence_PostToAnki(Guid id);
    }

    public class FacadeClient : IFacadeClient
    {
        private readonly IFlowController flowController;
        private readonly ITargetSequenceController targetSequenceController;
        private readonly ILanguageController languageController;
        private ISnapshotController snapshotController;
        private IMapper mapper;


        public FacadeClient()
        {
            this.flowController = Mef.Container.GetExportedValue<IFlowController>();
            this.targetSequenceController = Mef.Container.GetExportedValue<ITargetSequenceController>();
            this.languageController = Mef.Container.GetExportedValue<ILanguageController>();
            this.snapshotController = Mef.Container.GetExportedValue<ISnapshotController>();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(ClientMappingProfile)));

            mapper = configuration.CreateMapper();
        }

        public FlowModel Flow_Get(Guid id)
        {
            var f = flowController.Get(id);

            return mapper.Map<FlowModel>(f);
        }

        public Guid Flow_Create(string path)
        {
            return flowController.Create(path);
        }

        public Task TargetSequence_PostToAnki(Guid id)
        {
            return targetSequenceController.PostToAnki(id);
        }

        public List<LanguageDto> Language_GetAll()
        {
            return languageController.GetAll();
        }

        public void Snapshot_UpdateLanguage(Guid id, Guid languageId)
        {
            snapshotController.UpdateLanguage(id, languageId);
        }
    }
}
