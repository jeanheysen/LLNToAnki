using System.Collections.Generic;

namespace LLNToAnki.Business.Ports
{
    public interface ITextSplitter
    {
        IReadOnlyList<string> SplitOnTab(string text);
    }
}