using System.Collections.Generic;
using System.Text;

namespace LLNToAnki
{
    public interface IAnkiNoteExporter
    {
        void Export(string path, IReadOnlyList<IAnkiNote> notes);
    }

    public class AnkiNoteExporter : IAnkiNoteExporter
    {
        //FIELDS
        private readonly IFileWriter fileWriter;

        //CONSTRUCTOR
        public AnkiNoteExporter(IFileWriter fileWriter)
        {
            this.fileWriter = fileWriter;
        }


        //METHOD
        public void Export(string path, IReadOnlyList<IAnkiNote> notes)
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

        private void AppendContent(IAnkiNote note, StringBuilder sb)
        {
            sb.Append(note.Question);
            sb.Append("	");
            sb.Append(note.Answer);
        }

        private void Write(string path, StringBuilder sb)
        {
            this.fileWriter.Write(path, sb.ToString());
        }
    }
}
