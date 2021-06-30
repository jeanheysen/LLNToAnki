using LLNToAnki.Business;
using LLNToAnki.Business.Logic;
using LLNToAnki.Domain;
using System;

namespace LLNToAnki.Facade.Controllers
{
    [System.ComponentModel.Composition.Export(typeof(ISnapshotController)), System.Composition.Shared]
    public class SnapshotController : ISnapshotController
    {
        //dependencies
        private readonly ISnapshotBL snapshotBL;

        //constructor
        public SnapshotController()
        {
            snapshotBL = Mef.Container.GetExportedValue<ISnapshotBL>(); //todo dans le constructeur
        }

        //method
        public void UpdateLanguage(Guid id, Guid languageId)
        {
            snapshotBL.UpdateLanguage(id, languageId);
        }
    }
}
