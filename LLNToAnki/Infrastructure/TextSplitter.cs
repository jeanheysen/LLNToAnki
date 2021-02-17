using System.Collections.Generic;
using System.Linq;

public interface ITextSplitter
{
    IReadOnlyList<string> SplitOnTab(string text);
}

public class TextSplitter : ITextSplitter
{
    public IReadOnlyList<string> SplitOnTab(string text)
    {
        var separator = "\t";

        var r = text.Split(separator).ToList();

        return r;
    }
}
