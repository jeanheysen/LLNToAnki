using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LLNToAnki.BE.Ports
{
    public interface IConnectNoteBuilder
    {
        IConnectNote Build(IAnkiNote ankiNote);
        IConnectNote Build(string question, string answer, string after);
    }

    public interface IConnectNotePoster
    {
        Task<bool> Post(IConnectNote connectNote);
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
    }
}
