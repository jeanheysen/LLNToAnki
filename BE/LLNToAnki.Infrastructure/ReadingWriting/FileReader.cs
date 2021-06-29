using LLNToAnki.Business.Ports;
using System.IO;

namespace LLNToAnki.Infrastructure.ReadingWriting
{
    [System.ComponentModel.Composition.Export(typeof(IDataProvider)), System.Composition.Shared]
    public class FileReader : IDataProvider
    {
        public string GetAllText(string url)
        {
            return File.ReadAllText(url, System.Text.Encoding.UTF8);
        }
    }
}
