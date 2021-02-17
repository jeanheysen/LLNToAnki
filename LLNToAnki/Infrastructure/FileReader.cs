using System.IO;

namespace LLNToAnki
{
    public interface IFileReader
    {
        string ReadAllText(string url);
    }

    public class FileReader : IFileReader
    {
        public string ReadAllText(string url)
        {
            //TODO verifier l'encoding ici
            return File.ReadAllText(url, System.Text.Encoding.UTF8);
        }
    }
}
