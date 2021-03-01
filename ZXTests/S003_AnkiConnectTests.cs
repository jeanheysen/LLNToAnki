using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ZXTests
{
    public class connectNote
    {
        public string Action { get; set; }
        public int Version { get; set; }
        public Params Params { get; set; }
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
        public string fileName { get; set; }
        public string skipHash { get; set; }
        public List<string> fields { get; set; }
    }

    public class Process
    {
        public string GetJsonContent()
        {
            string content = "{\"action\": \"addNote\",\"version\": 6,\"params\": {\"note\": {\"deckName\": \"All\",\"modelName\": \"Full_Recto_verso_before_after_Audio\",\"fields\": {\"Question\": \"front content\",\"Answer\": \"back content\",\"After\" :\"blabla\"},\"options\": {\"allowDuplicate\": false,\"duplicateScope\": \"deck\",\"duplicateScopeOptions\": {\"deckName\": \"All\",\"checkChildren\": false}},\"picture\": [{\"url\": \"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/A_black_cat_named_Tilly.jpg/220px-A_black_cat_named_Tilly.jpg\",\"filename\": \"black_cat.jpg\",\"skipHash\": \"8d6e4646dfae812bf39651b59d7429ce\",\"fields\": [\"Back\"]}]}}}";
            return content;
        }

        public connectNote GetNote()
        {
            var o = new connectNote();
            o.Action = "addNote";
            o.Version = 6;
            o.Params = new Params();
            o.Params.note = new note();
            o.Params.note.deckName = "All";
            o.Params.note.modelName = "Full_Recto_verso_before_after_Audio";
            o.Params.note.fields = new fields();
            o.Params.note.fields.Question = "front content";
            o.Params.note.fields.Answer = "back content";
            o.Params.note.fields.After = "blabla";
            o.Params.note.options = new options();
            o.Params.note.options.allowDuplicate = false;
            o.Params.note.options.duplicateScope = "deck";
            o.Params.note.options.duplicateScopeOptions = new duplicateScopeOptions();
            o.Params.note.options.duplicateScopeOptions.deckName = "All";
            o.Params.note.options.duplicateScopeOptions.checkChildren = false;
            o.Params.note.picture = new List<picture>();
            var picture = new picture();
            picture.url = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/A_black_cat_named_Tilly.jpg/220px-A_black_cat_named_Tilly.jpg";
            picture.fileName = "black_cat.jpg";
            picture.skipHash = "8d6e4646dfae812bf39651b59d7429ce";
            picture.fields = new List<string>() { "Back" };
            o.Params.note.picture.Add(picture);
            return o;
        }
    }

    class S003_AnkiConnectTests
    {
        private Process process;

        public S003_AnkiConnectTests()
        {
            process = new Process();
        }

        [Test]
        public async Task T001_AddNotetoAnkiAndRetrieveIt()
        {
            //var note = GetNote();
            //var json = JsonConvert.SerializeObject(note).ToLower();

            //Arrange
            var data = new StringContent(process.GetJsonContent(), Encoding.UTF8, "application/json");
            var client = new HttpClient();

            //Act
            client.BaseAddress = new Uri("http://localhost:8765/");
            HttpResponseMessage response = await client.PostAsync("", data);

            //Assert - faire un get
        }

        [Test]
        public void T002_BuildContentWithJsonConverterReturnsSameResult()
        {
            //Arrange
            var note = process.GetNote();

            //Act
            var json = JsonConvert.SerializeObject(note).ToLower();

            //Assert
            StringAssert.AreEqualIgnoringCase(process.GetJsonContent(), json);
        }


    }
}
