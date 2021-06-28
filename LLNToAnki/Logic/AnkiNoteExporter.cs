using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
using System.Collections.Generic;
using System.Text;

namespace LLNToAnki.Business.Logic
{
    public interface IAnkiNoteExporter
    {
        void Export(string path, IReadOnlyList<AnkiNote> notes);
    }

    public class AnkiNoteExporter : IAnkiNoteExporter
    {
        //FIELDS
        private readonly IDataWriter dataWriter;
        private const string tab = "	";

        //CONSTRUCTOR
        public AnkiNoteExporter(IDataWriter dataWriter)
        {
            this.dataWriter = dataWriter;
        }


        //METHOD
        public void Export(string path, IReadOnlyList<AnkiNote> notes)
        {
            var sb = new StringBuilder();
            int i = 0;
            foreach (var note in notes)
            {
                if (i++ != 0) sb.AppendLine();
                AppendContent(note, sb);
            }

            Write(path, sb);
        }

        private void AppendContent(AnkiNote note, StringBuilder sb)
        {
            sb.Append("\"");
            sb.Append(note.Question.Replace("\"", "\"\""));
            sb.Append("\"");
            sb.Append(tab);
            sb.Append(note.Answer);
            sb.Append(tab);
            sb.Append(note.After);
            sb.Append(tab);
            sb.Append(note.Source);
            sb.Append(tab);
            sb.Append(note.Audio);
        }

        private void Write(string path, StringBuilder sb)
        {
            this.dataWriter.Write(path, sb.ToString());
        }
    }
}
