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

            AppendContent(notes[0], sb);

            sb.AppendLine();
            
            AppendContent(notes[1], sb);

            Write(path, sb);
        }

        public void Export(string path, AnkiNote note)
        {
            var sb = new StringBuilder();

            AppendContent(note, sb);

            Write(path, sb);
        }

        private static void AppendContent(AnkiNote note, StringBuilder sb)
        {
            sb.Append(note.Question);
            //sb.Append("	");
            //sb.Append(note.Before);
            sb.Append("	");
            sb.Append(note.Answer);
            //sb.Append("	");
            //sb.Append(note.After);
            //sb.Append("	");
            //sb.Append(note.Source);
            //sb.Append("	");
            //sb.Append(note.Audio);
            //sb.Append("	");
            //sb.Append(note.Mem_Image);
            //sb.Append("	");
            //sb.Append(note.Mem_Text);
            //sb.Append("	");
            //sb.Append(note.AddReverseQuestion);
            //sb.Append("	");
            //sb.Append(note.AddReverseAnswer);
        }

        private void Write(string path, StringBuilder sb)
        {
            this.fileWriter.Write(path, sb.ToString());
        }
    }
}
