using LLNToAnki.Facade.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LLNToAnki.WPF.Models
{
    public class FlowModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ObservableCollection<TargetSequenceDto> TargetSequences { get; set; }
    }
}
