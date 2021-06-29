using AutoMapper;
using LLNToAnki.Business;
using LLNToAnki.Business.Logic;
using LLNToAnki.Facade.Dto;
using System;

namespace LLNToAnki.Facade.Controllers
{
    public interface IFlowController
    {
        void Create(string path);
        FlowDto Get(Guid flowId);
    }

    public class FlowController : IFlowController
    {
        private readonly IFlowBL flowBL;
        private IMapper mapper;

        public FlowController()
        {
            flowBL = Mef.Container.GetExportedValue<IFlowBL>();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile)));

            mapper = configuration.CreateMapper();
        }

        public FlowDto Get(Guid flowId)
        {
            var flow = flowBL.GetById(flowId);

            return mapper.Map<FlowDto>(flow);
        }

        public void Create(string path)
        {

        }
    }
}
