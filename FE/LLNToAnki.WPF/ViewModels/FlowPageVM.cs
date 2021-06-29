using LLNToAnki.Facade.Controllers;
using LLNToAnki.Facade.Dto;
using LLNToAnki.WPF.Client;
using LLNToAnki.WPF.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace LLNToAnki.WPF.ViewModels
{
    public class FlowPageVM
    {
        //services
        private readonly IFacadeClient facadeClient;

        //ui bindings
        public FlowModel CurrentFlow { get; set; }
        public ObservableCollection<TargetSequenceDto> CurrentSequences { get; set; }


        //commands
        public ICommand AddFlowCommand { get; set; }

        public FlowPageVM(IFacadeClient facadeClient)
        {
            this.facadeClient = facadeClient;

            AddFlowCommand = new DelegateCommand<string>(s => AddFlow(s));
        }

        private void AddFlow(string s)
        {
            var path = Path.Combine(@"C:\Tmp\", s);

            facadeClient.Flow_Create(path);

            CurrentFlow = new FlowModel();

            CurrentFlow.TargetSequences = new ObservableCollection<TargetSequenceDto>()
            {
                new TargetSequenceDto() { Sequence = "blabla" }
            };
            CurrentSequences = CurrentFlow.TargetSequences;
        }
    }
}
