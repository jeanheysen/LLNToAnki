using System.IO;

namespace LLNToAnki
{
    public class Reader
    {

        public string Read(string url)
        {
            return File.ReadAllText(url);
        }
    }
}
