﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LLNToAnki.Infrastructure.AnkiConnect
{
    public class AnkiConnector
    {
        public connectNote GetNote(string question, string answer, string after)
        {
            var o = new connectNote();
            o.action = "addNote";
            o.version = 6;
            o.@params = new Params();
            o.@params.note = new note();
            o.@params.note.deckName = "All";
            o.@params.note.modelName = "Full_Recto_verso_before_after_Audio";
            o.@params.note.fields = new fields();
            o.@params.note.fields.Question = question;
            o.@params.note.fields.Answer = answer;
            o.@params.note.fields.After = after;
            o.@params.note.options = new options();
            o.@params.note.options.allowDuplicate = false;
            o.@params.note.options.duplicateScope = "deck";
            o.@params.note.options.duplicateScopeOptions = new duplicateScopeOptions();
            o.@params.note.options.duplicateScopeOptions.deckName = "All";
            o.@params.note.options.duplicateScopeOptions.checkChildren = false;
            o.@params.note.picture = new List<picture>();
            var picture = new picture();
            picture.url = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/A_black_cat_named_Tilly.jpg/220px-A_black_cat_named_Tilly.jpg";
            picture.filename = "black_cat.jpg";
            picture.skipHash = "8d6e4646dfae812bf39651b59d7429ce";
            picture.fields = new List<string>() { "Back" };
            o.@params.note.picture.Add(picture);
            return o;
        }
    }

    public class connectNote
    {
        public string action { get; set; }
        public int version { get; set; }
        public Params @params { get; set; }
    }

    public class Params
    {
        public note note { get; set; }
    }


    public class note
    {
        public string deckName { get; set; }
        public string modelName { get; set; }
        public fields fields { get; set; }
        public options options { get; set; }
        public List<picture> picture { get; set; }
    }
    public class fields
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
