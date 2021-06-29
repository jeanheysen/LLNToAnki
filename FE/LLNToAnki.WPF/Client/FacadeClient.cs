using AutoMapper;
using LLNToAnki.Facade.Controllers;
using LLNToAnki.WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LLNToAnki.WPF.Client
{
    public interface IFacadeClient
    {
        FlowModel Flow_GetById(Guid id);
    }

    public class FacadeClient : IFacadeClient
    {
        private readonly IFlowController flowController;
        private IMapper mapper;


        public FacadeClient(IFlowController flowController)
        {
            this.flowController = flowController;

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(ClientMappingProfile)));

            mapper = configuration.CreateMapper();
        }

        public FlowModel Flow_GetById(Guid id)
        {
            var f = flowController.GetById(id);

            return mapper.Map<FlowModel>(f);
        }
    }
}
