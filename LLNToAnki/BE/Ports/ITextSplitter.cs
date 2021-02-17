using System.Collections.Generic;

namespace LLNToAnki.BE.Ports
{
    public interface ITextSplitter
    {
        IReadOnlyList<string> SplitOnTab(string text);
    }
}