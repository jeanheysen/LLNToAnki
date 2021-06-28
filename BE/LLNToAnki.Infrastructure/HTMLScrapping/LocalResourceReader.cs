using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LLNToAnki.Infrastructure.HTMLScrapping
{
    public class LocalResourceReader
    {
        public Dictionary<string, string> Read(string fileName)
        {
            var r = new Dictionary<string, string>();
            var path = Path.Combine(MainFolderPath(), @"LLNToAnki\Infrastructure\Resources\", fileName);
            //C:\Users\felix\source\repos\LLNToAnki\LLNToAnki\Infrastructure\Resources\wmb_to_replace.json
            
            var d = JsonConvert.DeserializeObject(path);

            return r;
        }

        private string MainFolderPath()
        {
            var exePath = typeof(LocalResourceReader).Assembly.Location;
            var parent = Directory.GetParent(exePath).FullName;
            parent = Directory.GetParent(parent).FullName;
            parent = Directory.GetParent(parent).FullName;
            parent = Directory.GetParent(parent).FullName;
            parent = Directory.GetParent(parent).FullName;

            return parent;
        }
    }

}
