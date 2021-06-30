using LLNToAnki.Business.Ports;
using System.IO;

namespace LLNToAnki.Infrastructure.ReadingWriting
{
    [System.ComponentModel.Composition.Export(typeof(IDataImporter)), System.Composition.Shared]
    public class FileReader : IDataImporter
    {
        public string GetAllText(string url)
        {
            return File.ReadAllText(url, System.Text.Encoding.UTF8);
        }
    }
}
