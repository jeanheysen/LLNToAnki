using System.Collections.Generic;
using System.Linq;

public class Splitter
{
    public List<string> Split(string text)
    {
        var separator = "\t";

        var r = text.Split(separator).ToList();

        return r;
    }
}
