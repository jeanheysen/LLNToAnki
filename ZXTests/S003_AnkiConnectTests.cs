﻿using LLNToAnki.BE;
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
        public async Task T002_AddNotetoAnkiAndRetrieveIt()
        {
            //Arrange
            var data = new StringContent(process.GetJsonContent(), Encoding.UTF8, "application/json");

            //Act
            client.BaseAddress = new Uri("http://localhost:8765/");
            HttpResponseMessage response = await client.PostAsync("", data);

            //Assert - faire un get
        }

        [Test]
        public async Task T003_WorksWithBuildNoteConnectObject()
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
        public async Task T004_AddNoteWithHTML()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze.csv"));
            var splittedContent = TextSplitter.SplitOnTab(text);
            var item = WordItemBuilder.Build(new LLNItem() { HtmlContent = splittedContent[0] });
            var note = process.GetNote(item.ContextWithWordColored, item.Translation, item.EpisodTitle);
            var json = JsonConvert.SerializeObject(note);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri("http://localhost:8765/");

            //Act
            HttpResponseMessage response = await client.PostAsync("", data);

            //Assert - faire un get
        }

        [Test]
        public async Task T005_AddNoteWithQuestionFromCleanedHtmlForJson()
        {
            //Arrange
            string text = DataProvider.GetAllText(GetPathInData("SingleWord_squeeze_CleanedHtmlForJson.txt"));
            var note = process.GetNote(text, "this is the translation", "this is the episod title");
            var json = JsonConvert.SerializeObject(note);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            
            
            client.BaseAddress = new Uri("http://localhost:8765/");

            //Act
            HttpResponseMessage response = await client.PostAsync("", data);
        }
    }
}
