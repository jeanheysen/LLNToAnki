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
    class S003_AnkiConnectTests : BaseIntegrationTesting
    {
        private AnkiConnector process;
        private HttpClient client;

        public S003_AnkiConnectTests()
        {
            process = new AnkiConnector();
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8765/");
        }

        [Test]
        public void T001_BuildContentWithJsonConverterReturnsSameResultWithCase()
        {
            //Arrange
            var note = process.GetNote("front content", "back content", "blabla");

            //Act
            var json = JsonConvert.SerializeObject(note);

            //Assert
            Assert.AreEqual(process.GetJsonContent(), json);
        }

        [Test]
        public async Task T002_AddNoteFromJsonInTextWorksFine()
        {
            //Arrange
            var data = new StringContent(process.GetJsonContent(), Encoding.UTF8, "application/json");

            //Act
            client.BaseAddress = new Uri("http://localhost:8765/");
            HttpResponseMessage response = await client.PostAsync("", data);

            //Assert - faire un get
        }

        [Test]
        public async Task T003_AddNoteWithACatInTheQuestionWorksFine()
        {
            //Arrange
            var note = process.GetNote("<img src=\"black_cat.jpg\">", "back content", "blabla");
            var json = JsonConvert.SerializeObject(note);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri("http://localhost:8765/");

            //Act
            HttpResponseMessage response = await client.PostAsync("", data);

            //Assert - faire un get
        }


        [Test]
        public async Task T004_AddNoteWithQuestionCoucouInHTML_WorksFine()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("coucouHTML.txt"));
            var note = process.GetNote(text, "this is the translation", "this is the episod title");
            var json = JsonConvert.SerializeObject(note);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await client.PostAsync("", data);
        }




        [Test]
        public async Task T005_AddNoteWithQuestionWithSimpleHtmlInJson_WorksFine()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("simpleHtmlForJson.txt"));
            var note = process.GetNote(text, "this is the translation", "this is the episod title");
            var json = JsonConvert.SerializeObject(note);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await client.PostAsync("", data);
        }

        [Test]
        public async Task T006_AddNoteSqueezeTriggerFromCleanedHtml_WorksFine()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze_CleanedHtmlForJson.txt"));
            var note = process.GetNote(text, "this is the translation", "this is the episod title");
            var json = JsonConvert.SerializeObject(note);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            
            //Act
            HttpResponseMessage response = await client.PostAsync("", data);
        }

        [Test]
        public async Task T007_AddNote_CleanedWordReferenceTable()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("WordReference_eyeBall_InnerHTML.txt"));
            var note = process.GetNote(text, "this is the translation", "this is the episod title");
            var json = JsonConvert.SerializeObject(note);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await client.PostAsync("", data);
        }
    }
}
