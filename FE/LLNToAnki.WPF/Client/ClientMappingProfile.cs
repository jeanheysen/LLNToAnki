using LLNToAnki.Facade.Dto;
using LLNToAnki.WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LLNToAnki.WPF.Client
{
    public class ClientMappingProfile : AutoMapper.Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<FlowDto, FlowModel>().MaxDepth(3);
            CreateMap<FlowModel, FlowDto>().MaxDepth(3);
        }
    }
}
