using LLNToAnki.Domain;
using LLNToAnki.Facade.Dto;

namespace LLNToAnki.Facade.Controllers
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<FlowDto, Flow>().MaxDepth(3);
            CreateMap<Flow, FlowDto>().MaxDepth(3);

            CreateMap<TargetSequenceDto, TargetSequence>().MaxDepth(3);
            CreateMap<TargetSequence, TargetSequenceDto>().MaxDepth(3);
        }
    }
}
