using LLNToAnki.BE;
using System.IO;

namespace LLNToAnki.Infrastructure
{

    public class FileWriter : IFileWriter
    {
        public void Write(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
