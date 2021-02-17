﻿using LLNToAnki.BE.Ports;
using System.IO;

namespace LLNToAnki.Infrastructure
{

    public class FileWriter : IDataWriter
    {
        public void Write(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
