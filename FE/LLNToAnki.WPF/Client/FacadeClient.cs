using AutoMapper;
using LLNToAnki.Facade.Controllers;
using LLNToAnki.Facade.Dto;
using LLNToAnki.WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LLNToAnki.WPF.Client
{
    public interface IFacadeClient
    {
        Guid Flow_Create(string path);
        FlowModel Flow_Get(Guid id);
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

        public FlowModel Flow_Get(Guid id)
        {
            var f = flowController.Get(id);

            return mapper.Map<FlowModel>(f);
        }

        public Guid Flow_Create(string path)
        {
            return flowController.Create(path);
        }
    }
}
