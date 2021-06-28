using AutoMapper;
using LLNToAnki.Business.Logic;
using LLNToAnki.Facade.Dto;
using System;

namespace LLNToAnki.Facade.Controllers
{
    public interface IFlowController
    {
        FlowDto GetById(Guid flowId);
    }

    public class FlowController : IFlowController
    {
        private readonly IFlowBL flowBL;
        private IMapper mapper;

        public FlowController(IFlowBL flowBL)
        {
            this.flowBL = flowBL;

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile)));

            mapper = configuration.CreateMapper();
        }

        public FlowDto GetById(Guid flowId)
        {
            var flow = flowBL.GetById(flowId);

            return mapper.Map<FlowDto>(flow);
        }
    }
}
