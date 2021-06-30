using LLNToAnki.Business;
using LLNToAnki.Business.Logic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LLNToAnki.Facade.Controllers
{
    [System.ComponentModel.Composition.Export(typeof(ITargetSequenceController)), System.Composition.Shared]
    public class TargetSequenceController : ITargetSequenceController
    {
        //dependencies
        private readonly ITargetSequenceBL targetSequenceBL;

        //constructor
        public TargetSequenceController()
        {
            targetSequenceBL = Mef.Container.GetExportedValue<ITargetSequenceBL>(); //todo dans le constructeur
        }

        //method
        public async Task PostToAnki(Guid id)
        {
            await targetSequenceBL.PostToAnki(id);
        }
    }
}
