using System.Collections.Generic;
using System.Text;

namespace LLNToAnki
{
    public class AnkiNoteCsvExporter
    {
        private readonly IFileWriter fileWriter;

        public AnkiNoteCsvExporter(IFileWriter fileWriter)
        {
            this.fileWriter = fileWriter;
        }

        public void Export(string path, List<AnkiNote> notes)
        {
            var sb = new StringBuilder();
            int i = 0;
            foreach (var note in notes)
            {
                if(i++!=0) sb.AppendLine();
                AppendContent(note, sb);
            }

            Write(path, sb);
        }

        public void Export(string path, AnkiNote note)
        {
            var sb = new StringBuilder();

            AppendContent(note, sb);

            Write(path, sb);
        }

        private void AppendContent(AnkiNote note, StringBuilder sb)
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
