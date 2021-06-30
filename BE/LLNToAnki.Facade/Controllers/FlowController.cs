using AutoMapper;
using LLNToAnki.Business;
using LLNToAnki.Business.Logic;
using LLNToAnki.Facade.Dto;
using System;

namespace LLNToAnki.Facade.Controllers
{
    public interface IFlowController
    {
        Guid Create(string path);
        FlowDto Get(Guid flowId);
    }

    [System.ComponentModel.Composition.Export(typeof(IFlowController)), System.Composition.Shared]
    public class FlowController : IFlowController
    {
        private readonly IFlowBL flowBL;
        private IMapper mapper;

        public FlowController()
        {
            flowBL = Mef.Container.GetExportedValue<IFlowBL>(); //todo dans le constructeur

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile)));

            mapper = configuration.CreateMapper();
        }

        public FlowDto Get(Guid flowId)
        {
            var flow = flowBL.GetById(flowId);

            return mapper.Map<FlowDto>(flow);
        }

        public Guid Create(string path)
        {
            return flowBL.Create(path);
        }
    }
}
