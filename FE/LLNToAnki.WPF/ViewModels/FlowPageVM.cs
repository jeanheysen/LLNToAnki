using LLNToAnki.Facade.Controllers;
using LLNToAnki.Facade.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LLNToAnki.WPF.ViewModels
{
    public class FlowPageVM
    {
        private readonly IFlowController flowController;

        public ObservableCollection<FlowDto> Flows { get; set; }

        public FlowPageVM(IFlowController flowController)
        {
            this.flowController = flowController;
        }
    }
}
