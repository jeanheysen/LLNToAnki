using LLNToAnki.BE.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace LLNToAnki.Infrastructure.HTMLScrapping
{
    public class WordReferenceTranslationsProvider : ITranslationsProvider
    {
        private HTMLWebsiteReader websiteReader;
        private HTMLScraper scraper;

        public WordReferenceTranslationsProvider()
        {
            websiteReader = new HTMLWebsiteReader();

            scraper = new HTMLScraper();
        }

        public string GetTranslations(string word)
        {
            var url = $"https://www.wordreference.com/enfr/{word}";

            var mainNode = websiteReader.GetHTML(url);

            var node = scraper.GetNodeByNameAndAttribute(mainNode, "table", "class");

            return CleanFromSyntaxExplanations(node.ParentNode.InnerHtml);
        }

        private string CleanFromSyntaxExplanations(string content)
        {
            var toRemove = new List<string>();

            toRemove.Add(": Refers to person, place, thing, quality, etc.");
            toRemove.Add(": Verb taking a direct object--for example, \"Say something.\" \"She found the cat.\"");
            toRemove.Add("nom masculin: s'utilise avec les articles \"le\", \"l'\"(devant une voyelle ou un h muet), \"un\" Ex : garçon - nm > On dira \"le garçon\" ou \"un garçon\".");
            toRemove.Add(": verbe qui s'utilise avec un complément d'objet direct (COD). Ex : \"J'écris une lettre\". \"Elle a retrouvé son chat\".");
            toRemove.Add(": Verb not taking a direct object--for example, \"She jokes.\" \"He has arrived.\"");
            toRemove.Add(": verbe qui s'utilise sans complément d'objet direct (COD). Ex : \"Il est parti.\" \"Elle a ri.\"");
            toRemove.Add(": verbe qui s'utilise avec le pronom réfléchi \"se\", qui s'accorde avec le sujet. Ex : se regarder : \"Je me regarde dans le miroir. Tu te regardes dans le miroir.\". Les verbes pronominaux se conjuguent toujours avec l'auxiliaire \"être\". Ex : \"Elle a lavé la voiture\" mais \"Elle s'est lavée.\"");
            toRemove.Add(": Describes a noun or pronoun--for example, \"a tall girl,\" \"an interesting book,\" \"a big house.\"	");
            toRemove.Add(": modifie un nom. Il est généralement placé après le nom et s'accorde avec le nom (ex : un ballon bleu, une balle bleue). En général, seule la forme au masculin singulier est donnée. Pour former le féminin, on ajoute \"e\" (ex : petit > petite) et pour former le pluriel, on ajoute \"s\" (ex : petit > petits). Pour les formes qui sont \"irrégulières\" au féminin, celles-ci sont données (ex : irrégulier, irrégulière > irrégulier = forme masculine, irrégulière = forme féminine)");
            toRemove.Add(": s'utilise avec les articles \"le\", \"l'\" (devant une voyelle ou un h muet), \"un\". Ex : garçon - nm > On dira \"le garçon\" ou \"un garçon\".");
            toRemove.Add(": s'utilise avec les articles \"la\", \"l'\" (devant une voyelle ou un h muet), \"une\". Ex : fille - nf > On dira \"la fille\" ou \"une fille\". Avec un nom féminin, l'adjectif s'accorde. En général, on ajoute un \"e\" à l'adjectif. Par exemple, on dira \"une petite fille\".");

            //: Verb not taking a direct object--for example, "She jokes." "He has arrived."
            //: verbe qui s'utilise sans complément d'objet direct (COD). Ex : "Il est parti." "Elle a ri."
            //: verbe qui s'utilise avec le pronom réfléchi "se", qui s'accorde avec le sujet. Ex : se regarder : "Je me regarde dans le miroir. Tu te regardes dans le miroir.". Les verbes pronominaux se conjuguent toujours avec l'auxiliaire "être". Ex : "Elle a lavé la voiture" mais "Elle s'est lavée."

            foreach (var s in toRemove)
            {
                if (content.Contains(s)) content = content.Replace(s, "");
            }
            return content;
        }
    }
}
