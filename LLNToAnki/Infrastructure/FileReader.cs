using LLNToAnki.BE;
using System.IO;

namespace LLNToAnki.Infrastructure
{

    public class FileReader : IFileReader
    {
        public string ReadAllText(string url)
        {
            //TODO verifier l'encoding ici
            return File.ReadAllText(url, System.Text.Encoding.UTF8);
        }
    }
}
