using LLNToAnki.BE.Ports;
using System.Collections.Generic;

namespace LLNToAnki.Infrastructure.AnkiConnect
{
    public class connectNote : IConnectNote
    {
        public string action { get; set; }
        public int version { get; set; }
        public IParams @params { get; set; }

        public connectNote()
        {
            @params = new Params();
            action = "addNote";
            version = 6;
        }
    }

    public class Params : IParams
    {
        public INote note { get; set; }

        public Params()
        {
            note = new note
            {
                deckName = "All",
                modelName = "Full_Recto_verso_before_after_Audio"
            };
        }
    }

    public class note : INote
    {
        public string deckName { get; set; }
        public string modelName { get; set; }
        public IFields fields { get; set; }
        public options options { get; set; }
        public List<picture> picture { get; set; }

        public note()
        {
            fields = new fields();
            options = new options();
            options.allowDuplicate = false;
            options.duplicateScope = "deck";
            options.duplicateScopeOptions = new duplicateScopeOptions();
            options.duplicateScopeOptions.deckName = "All";
            options.duplicateScopeOptions.checkChildren = false;

            picture = new List<picture>();
            var pic = new picture();
            pic.url = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/A_black_cat_named_Tilly.jpg/220px-A_black_cat_named_Tilly.jpg";
            pic.filename = "black_cat.jpg";
            pic.skipHash = "8d6e4646dfae812bf39651b59d7429ce";
            pic.fields = new List<string>() { "Back" };
            picture.Add(pic);
        }
    }

    public class fields : IFields
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string After { get; set; }
    }
    
    public class options
    {
        public bool allowDuplicate { get; set; }
        public string duplicateScope { get; set; }
        public duplicateScopeOptions duplicateScopeOptions { get; set; }
    }

    public class duplicateScopeOptions
    {
        public string deckName { get; set; }
        public bool checkChildren { get; set; }
    }

    public class picture
    {
        public string url { get; set; }
        public string filename { get; set; }
        public string skipHash { get; set; }
        public List<string> fields { get; set; }
    }
}
