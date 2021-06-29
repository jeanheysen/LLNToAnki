using LLNToAnki.Facade.Controllers;
using LLNToAnki.Facade.Dto;
using LLNToAnki.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LLNToAnki.WPF.ViewModels
{
    public class FlowPageVM
    {
        private readonly IFlowController flowController;

        public FlowModel CurrentFlow { get; set; }

        public FlowPageVM(IFlowController flowController)
        {
            this.flowController = flowController;
        }
    }
}
