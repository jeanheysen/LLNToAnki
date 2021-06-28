using LLNToAnki.Business.Ports;
using System.Collections.Generic;
using System.Linq;

namespace LLNToAnki.Business.Logic
{

    public class TextSplitter : ITextSplitter
{
    public IReadOnlyList<string> SplitOnTab(string text)
    {
        var separator = "\t";

        var r = text.Split(separator).ToList();

        return r;
    }
}
}