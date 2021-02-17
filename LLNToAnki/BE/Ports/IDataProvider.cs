namespace LLNToAnki.BE.Ports
{
    public interface IDataProvider
    {
        string ReadAllText(string url);
    }
}
