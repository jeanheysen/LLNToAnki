using LLNToAnki.BE.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace LLNToAnki.Infrastructure.HTMLScrapping
{
    public class WordReferenceTranslationsProvider : ITranslationsProvider
    {
        private readonly IURLBuilder urlBuilder;
        private HTMLWebsiteReader websiteReader;
        private HTMLScraper scraper;

        public WordReferenceTranslationsProvider(IURLBuilder urlBuilder)
        {
            websiteReader = new HTMLWebsiteReader();

            scraper = new HTMLScraper();
            this.urlBuilder = urlBuilder;
        }

        public string GetTranslations(string word)
        {
            var url = urlBuilder.OnlineWordReference(word);
            
            var mainNode = websiteReader.GetHTML(url);

            var node = scraper.GetNodeByNameAndAttribute(mainNode, "table", "class");

            return CleanFromSyntaxExplanations(node.ParentNode.InnerHtml);
        }

        private string CleanFromSyntaxExplanations(string content)
        {
            var toRemove = new List<string>();

            toRemove.Add(": Refers to person, place, thing, quality, etc.");
            toRemove.Add(": s'utilise avec les articles <b>\"le\", \"l'\" </b>(devant une voyelle ou un h muet), <b>\"un\"</b>. <i>Ex : garçon - nm &gt; On dira \"<b>le</b> garçon\" ou \"<b>un</b> garçon\". </i>");
            toRemove.Add(": Verb taking a direct object--for example, \"<b>Say</b> something.\" \"She <b>found</b> the cat.\"");
            toRemove.Add(": verbe qui s'utilise avec un complément d'objet direct (COD). <i>Ex : \"J'<b>écris</b> une lettre\". \"Elle <b>a retrouvé</b> son chat\".</i>");
            //toRemove.Add("<td style=\"text-align:right;\"><a target=\"WRsug\" title=\"Is something important missing? Report an error or suggest an improvement.\" href=\"https://forum.wordreference.com/forums/dictionary-error-reports-and-suggestions.30/post-thread?prefix_id=25&amp;title=eyeball\"><span class=\"ph\" data-ph=\"sReportError\">Un oubli important ? Signalez une erreur ou suggérez une amélioration.</span></a></td>");

            toRemove.Add(": Verb not taking a direct object--for example, \"She <b>jokes</b>.\" \"He <b>has arrived</b>.\"");
            toRemove.Add(": verbe qui s'utilise sans complément d'objet direct (COD). <i>Ex : \"Il est <b>parti</b>.\" \"Elle <b>a ri</b>.\"</i>");
            toRemove.Add(": verbe qui s'utilise avec le pronom réfléchi \"se\", qui s'accorde avec le sujet. <i>Ex : <b>se regarder</b> : \"Je <b>me</b> regarde dans le miroir. Tu <b>te</b> regardes dans le miroir.\"</i>. Les verbes pronominaux se conjuguent toujours avec l'auxiliaire \"être\". <i>Ex : \"Elle <b>a</b> lavé la voiture\" mais \"Elle s'<b>est</b> lavée.\"</i>");

            foreach (var s in toRemove)
            {
                if (content.Contains(s)) content = content.Replace(s, "");
            }
            return content;
        }
    }
}
