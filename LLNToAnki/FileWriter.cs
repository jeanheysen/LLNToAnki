using System.IO;

namespace LLNToAnki
{
    public interface IFileWriter
    {
        void Write(string path, string content);
    }

    public class FileWriter : IFileWriter
    {
        public void Write(string path, string content)
        {
            File.WriteAllText(path, content, System.Text.Encoding.ASCII);
        }
    }
}
