﻿namespace LLNToAnki.Infrastructure.URL
{
    public class MijnWordenboekURLBuilder : IURLBuilder
    {
        public string CreateURL(string word)
        {
            return $"https://www.mijnwoordenboek.nl/vertaal/NL/FR/{word}";
        }
    }
}
