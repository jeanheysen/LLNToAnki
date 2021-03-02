using LLNToAnki.BE;
using LLNToAnki.BE.Ports;
using System.Collections.Generic;

namespace LLNToAnki.Infrastructure.AnkiConnect
{
    

    public class ConnectNoteBuilder : IConnectNoteBuilder
    {
        public IConnectNote Build(IAnkiNote ankiNote)
        {
            return Build(ankiNote.Question, ankiNote.Answer, ankiNote.After);
        }


        public IConnectNote Build(string question, string answer, string after)
        {
            var o = new connectNote();
          
            o.@params.note.fields.Question = question;
            o.@params.note.fields.Answer = answer;
            o.@params.note.fields.After = after;
            
            return o;
        }
    }

  
}
