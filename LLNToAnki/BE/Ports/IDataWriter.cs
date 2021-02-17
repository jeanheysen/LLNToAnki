namespace LLNToAnki.BE.Ports
{
    public interface IDataWriter
    {
        void Write(string path, string content);
    }
}
