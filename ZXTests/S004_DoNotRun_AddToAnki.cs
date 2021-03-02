using LLNToAnki.BE;
using LLNToAnki.Infrastructure.AnkiConnect;
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
    class S004_DoNotRun_AddToAnki : BaseIntegrationTesting
    {
        private ConnectNoteBuilder connectNoteBuilder;
        private ConnectNotePoster connectNotePoster;
        private HttpClient client;

        public S004_DoNotRun_AddToAnki()
        {
            connectNoteBuilder = new ConnectNoteBuilder();
            connectNotePoster = new ConnectNotePoster();
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8765/");
        }

        public string GetJsonContent()
        {
            string content = "{\"action\":\"addNote\",\"version\":6,\"params\":{\"note\":{\"deckName\":\"All\",\"modelName\":\"Full_Recto_verso_before_after_Audio\",\"fields\":{\"Question\":\"front content\",\"Answer\":\"back content\",\"After\":\"blabla\"},\"options\":{\"allowDuplicate\":false,\"duplicateScope\":\"deck\",\"duplicateScopeOptions\":{\"deckName\":\"All\",\"checkChildren\":false}},\"picture\":[{\"url\":\"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/A_black_cat_named_Tilly.jpg/220px-A_black_cat_named_Tilly.jpg\",\"filename\":\"black_cat.jpg\",\"skipHash\":\"8d6e4646dfae812bf39651b59d7429ce\",\"fields\":[\"Back\"]}]}}}";
            return content;
        }


        [Test]
        public async Task T001_AddNoteFromJsonInTextWorksFine()
        {
            //Arrange
            var data = new StringContent(GetJsonContent(), Encoding.UTF8, "application/json");

            //Act
            client.BaseAddress = new Uri("http://localhost:8765/");
            HttpResponseMessage response = await client.PostAsync("", data);

            //Assert - faire un get
        }

        [Test]
        public async Task T002_AddNoteWithACatInTheQuestionWorksFine()
        {
            //Arrange
            var note = connectNoteBuilder.Build("<img src=\"black_cat.jpg\">", "back content", "blabla");

            //Act
            await connectNotePoster.Post(note);

            //Assert
        }

        [Test]
        public async Task T003_AddNoteWithQuestionCoucouInHTML_WorksFine()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("coucouHTML.txt"));
            var note = connectNoteBuilder.Build(text, "this is the translation", "this is the episod title");

            //Act
            await connectNotePoster.Post(note);

        }

        [Test]
        public async Task T004_AddNoteWithQuestionWithSimpleHtmlInJson_WorksFine()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("simpleHtmlForJson.txt"));
            var note = connectNoteBuilder.Build(text, "this is the translation", "this is the episod title");

            //Act
            await connectNotePoster.Post(note);

        }

        [Test]
        public async Task T005_AddNoteSqueezeTriggerFromCleanedHtml_WorksFine()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze_CleanedHtmlForJson.txt"));
            var note = connectNoteBuilder.Build(text, "this is the translation", "this is the episod title");

            //Act
            await connectNotePoster.Post(note);

        }

        [Test]
        public async Task T006_AddNote_CleanedWordReferenceTable_WorksFine()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("WordReference_eyeBall_TableHTML.txt"));
            var note = connectNoteBuilder.Build(text, "this is the translation", "this is the episod title");

            //Act
            await connectNotePoster.Post(note);
        }
    }

}
