using System.IO;

namespace LLNToAnki
{
    public class Reader
    {

        public string ReadFileFromPath(string url)
        {
            return File.ReadAllText(url, System.Text.Encoding.UTF8);
        }
    }
}
