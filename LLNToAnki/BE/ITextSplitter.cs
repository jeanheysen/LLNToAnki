using System.Collections.Generic;

namespace LLNToAnki.BE
{
    public interface ITextSplitter
{
    IReadOnlyList<string> SplitOnTab(string text);
}
}