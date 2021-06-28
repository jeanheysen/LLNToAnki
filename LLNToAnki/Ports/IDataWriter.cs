namespace LLNToAnki.Business.Ports
{
    public interface IDataWriter
    {
        void Write(string path, string content);
    }
}
