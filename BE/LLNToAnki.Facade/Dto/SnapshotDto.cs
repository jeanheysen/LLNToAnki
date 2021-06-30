using System;

namespace LLNToAnki.Facade.Dto
{
    public class SnapshotDto
    {
        public Guid Id { get; set; }

        public string HtmlContent { get; set; }

        public string Audio { get; set; }

        public string Tag { get; set; }

        public LanguageDto Language { get; set; }
    }
}
