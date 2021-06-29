using LLNToAnki.Facade.Controllers;
using LLNToAnki.Facade.Dto;
using LLNToAnki.WPF.Client;
using LLNToAnki.WPF.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace LLNToAnki.WPF.ViewModels
{
    public class FlowPageVM : INotifyPropertyChanged
    {
        //services
        private readonly IFacadeClient facadeClient;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //ui bindings
        private FlowModel currentFlow;
        public FlowModel CurrentFlow
        {
            get
            {
                return currentFlow;
            }
            set
            {
                currentFlow = value;
                OnPropertyChanged("CurrentFlow");
            }
        }

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

            var id = facadeClient.Flow_Create(path);

            CurrentFlow = facadeClient.Flow_Get(id);
        }
    }
}
