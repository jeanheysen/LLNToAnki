using LLNToAnki.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LLNToAnki.Business.Logic
{
    public interface IFlowBL
    {
        Flow GetById(Guid id);
    }

    [System.ComponentModel.Composition.Export(typeof(IFlowBL)), System.Composition.Shared]
    public class FlowBL : IFlowBL
    {
        private List<Flow> flows;

        public FlowBL()
        {

        }

        public Flow GetById(Guid id)
        {
            return flows.FirstOrDefault(f => f.Id == id);
        }
    }
}