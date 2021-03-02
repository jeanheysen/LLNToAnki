using LLNToAnki.BE;
using LLNToAnki.Infrastructure.AnkiConnect;
using Moq;
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
        private Mock<AnkiNote> ankiNoteMock;

        public S004_DoNotRun_AddToAnki()
        {
            connectNoteBuilder = new ConnectNoteBuilder();
            connectNotePoster = new ConnectNotePoster();
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8765/");

            ankiNoteMock = new Mock<AnkiNote>() { DefaultValue = DefaultValue.Mock };
            ankiNoteMock.SetupGet(a => a.Question).Returns("front content");
            ankiNoteMock.SetupGet(a => a.Answer).Returns("back content");
            ankiNoteMock.SetupGet(a => a.After).Returns("blabla");
            var note = connectNoteBuilder.Build(ankiNoteMock.Object);
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
            ankiNoteMock.SetupGet(a => a.Question).Returns("<img src=\"black_cat.jpg\">");
            var note = connectNoteBuilder.Build(ankiNoteMock.Object);

            //Act
            await connectNotePoster.Post(note);

            //Assert
        }

        [Test]
        public async Task T003_AddNoteWithQuestionCoucouInHTML_WorksFine()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("coucouHTML.txt"));
            ankiNoteMock.SetupGet(a => a.Question).Returns(text);
            var ankiNote = connectNoteBuilder.Build(ankiNoteMock.Object);

            //Act
            await connectNotePoster.Post(ankiNote);

        }

        [Test]
        public async Task T004_AddNoteWithQuestionWithSimpleHtmlInJson_WorksFine()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("simpleHtmlForJson.txt"));
            ankiNoteMock.SetupGet(a => a.Question).Returns(text);
            var ankiNote = connectNoteBuilder.Build(ankiNoteMock.Object);

            //Act
            await connectNotePoster.Post(ankiNote);

        }

        [Test]
        public async Task T005_AddNoteSqueezeTriggerFromCleanedHtml_WorksFine()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze_CleanedHtmlForJson.txt"));
            ankiNoteMock.SetupGet(a => a.Question).Returns(text);
            var ankiNote = connectNoteBuilder.Build(ankiNoteMock.Object);

            //Act
            await connectNotePoster.Post(ankiNote);

        }

        [Test]
        public async Task T006_AddNote_CleanedWordReferenceTable_WorksFine()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("WordReference_eyeBall_TableHTML.txt"));
            ankiNoteMock.SetupGet(a => a.Question).Returns(text);
            var ankiNote = connectNoteBuilder.Build(ankiNoteMock.Object);

            //Act
            await connectNotePoster.Post(ankiNote);
        }
    }

}
