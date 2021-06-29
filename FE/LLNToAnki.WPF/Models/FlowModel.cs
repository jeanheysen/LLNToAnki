using LLNToAnki.Facade.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace LLNToAnki.WPF.Models
{
    public class FlowModel: INotifyPropertyChanged
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        private ObservableCollection<TargetSequenceDto> targetSequences;
        public ObservableCollection<TargetSequenceDto> TargetSequences 
        {
            get
            {
                return targetSequences;
            }
            set
            {
                targetSequences = value;
                OnPropertyChanged("TargetSequences");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
