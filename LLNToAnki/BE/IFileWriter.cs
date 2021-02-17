namespace LLNToAnki.BE
{
    public interface IFileWriter
    {
        void Write(string path, string content);
    }
}
