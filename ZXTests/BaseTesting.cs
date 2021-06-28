using System.IO;

namespace ZXTests
{
    public class BaseTesting
    {
        protected string GetPathInData(string fileNameWithExtension)
        {
            return Path.Combine(DataDirectory, fileNameWithExtension);
        }

        protected string GetPathInTmp(string fileNameWithExtension)
        {
            return Path.Combine(TmpDirectory, fileNameWithExtension);
        }


        public string TmpDirectory => Path.Combine(DataDirectory, "tmp");

        public string DataDirectory
        {
            get
            {
                var current = Directory.GetCurrentDirectory();

                string parent = current;
                parent = Directory.GetParent(parent).FullName;
                parent = Directory.GetParent(parent).FullName;
                parent = Directory.GetParent(parent).FullName;

                return Path.Combine(parent, "Data");
            }
        }

        public void ClearTmp()
        {
            var files = Directory.GetFiles(TmpDirectory);

            foreach (var f in files)
            {
                File.Delete(f);
            }
        }
    }
}