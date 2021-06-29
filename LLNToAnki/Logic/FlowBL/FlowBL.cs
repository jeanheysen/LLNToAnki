using LLNToAnki.Domain;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;

namespace LLNToAnki.Business.Logic
{
    public interface IFlowBL
    {
        Flow GetById(Guid id);
    }

    [Export(typeof(IFlowBL))]
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