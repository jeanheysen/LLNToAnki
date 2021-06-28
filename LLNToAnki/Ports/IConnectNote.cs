using LLNToAnki.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LLNToAnki.Business.Ports
{
    public interface IConnectNoteBuilder
    {
        IConnectNote Build(AnkiNote ankiNote);
    }

    public interface IConnectNotePoster
    {
        Task<string> Post(IConnectNote connectNote);
    }

    public interface IConnectNote
    {
        IParams @params { get; set; }
    }

    public interface IParams
    {
        INote note { get; set; }
    }

    public interface INote
    {
        IFields fields { get; set; }
    }

    public interface IFields
    {
        string After { get; set; }
        string Answer { get; set; }
        string Question { get; set; }
        string Source { get; set; }
        string Audio { get; set; }
    }
}
