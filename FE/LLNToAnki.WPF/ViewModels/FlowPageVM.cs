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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        public List<LanguageDto> Languages { get; set; }

        //commands
        public ICommand AddFlowCommand { get; set; }
        public ICommand SendSequencesCommand { get; set; }
        public ICommand ChangeLanguageCommand { get; set; }
        

        public FlowPageVM(IFacadeClient facadeClient)
        {
            this.facadeClient = facadeClient;

            AddFlowCommand = new DelegateCommand<string>(s => AddFlow(s));
            SendSequencesCommand = new DelegateCommand(async () => await SendSequences());
            ChangeLanguageCommand = new DelegateCommand<LanguageDto>(l => ChangeLanguage(l));

            Languages = facadeClient.Language_GetAll();
        }

        private void ChangeLanguage(LanguageDto l)
        {
            foreach (var item in CurrentFlow.TargetSequences.Select(s=>s.Snapshot))
            {
                facadeClient.Snapshot_UpdateLanguage(item.Id, l.Id);
            }
        }

        private async Task SendSequences()
        {
            foreach (var id in CurrentFlow.TargetSequences.Select(s => s.Id))
            {
                await facadeClient.TargetSequence_PostToAnki(id);
            }
        }

        private void AddFlow(string s)
        {
            var path = Path.Combine(@"C:\Tmp\", s);

            var id = facadeClient.Flow_Create(path);

            CurrentFlow = facadeClient.Flow_Get(id);
        }
    }
}
