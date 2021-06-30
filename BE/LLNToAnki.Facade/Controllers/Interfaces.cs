using LLNToAnki.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LLNToAnki.Facade.Controllers
{
    public interface ITargetSequenceController
    {
        Task PostToAnki(Guid id);
    }

    public interface ISnapshotController
    {
        void UpdateLanguage(Guid id, Guid languageId);
    }
}
