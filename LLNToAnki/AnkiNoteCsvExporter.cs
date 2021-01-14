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

        public void Export(string path, AnkiNote note)
        {
            var sb = new StringBuilder();
            sb.Append(note.Question);
            sb.Append("	");
            sb.Append(note.Before);
            sb.Append("	");
            sb.Append(note.Answer);
            this.fileWriter.Write(path, sb.ToString());
        }
    }
}
