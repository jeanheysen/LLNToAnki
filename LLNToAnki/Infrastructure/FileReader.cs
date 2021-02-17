﻿using LLNToAnki.BE.Ports;
using System.IO;

namespace LLNToAnki.Infrastructure
{
    public class FileReader : IDataProvider
    {
        public string GetAllText(string url)
        {
            //TODO verifier l'encoding ici
            return File.ReadAllText(url, System.Text.Encoding.UTF8);
        }
    }
}