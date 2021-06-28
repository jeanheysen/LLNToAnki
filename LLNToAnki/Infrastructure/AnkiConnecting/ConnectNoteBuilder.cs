using LLNToAnki.BE;
using LLNToAnki.BE.Ports;
using LLNToAnki.Domain;
using System.Collections.Generic;

namespace LLNToAnki.Infrastructure.AnkiConnecting
{
    public class ConnectNoteBuilder : IConnectNoteBuilder
    {
        public IConnectNote Build(IAnkiNote ankiNote)
        {
            return Build(ankiNote.Question, ankiNote.Answer, ankiNote.After, ankiNote.Source, ankiNote.Audio);
        }

        private IConnectNote Build(string question, string answer, string after, string source, string audio)
        {
            var o = new connectNote();

            o.@params.note.fields.Question = question;
            o.@params.note.fields.Answer = answer;
            o.@params.note.fields.After = after;
            o.@params.note.fields.Source = source;
            o.@params.note.fields.Audio = audio;

            return o;
        }
    }


}
