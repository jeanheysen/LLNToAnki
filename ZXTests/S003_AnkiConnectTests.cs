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
    class S003_AnkiConnectTests
    {
        private AnkiConnector process;

        public S003_AnkiConnectTests()
        {
            process = new AnkiConnector();
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
            var client = new HttpClient();

            //Act
            client.BaseAddress = new Uri("http://localhost:8765/");
            HttpResponseMessage response = await client.PostAsync("", data);

            //Assert - faire un get
        }

        [Test]
        public async Task T003_WorksWithBuildNoteConnectObject()
        {
            //Arrange
            var note = process.GetNote("front content", "back content", "blabla");
            var json = JsonConvert.SerializeObject(note);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8765/");

            //Act
            HttpResponseMessage response = await client.PostAsync("", data);

            //Assert - faire un get
        }

        [Test]
        public async Task T004_AddNoteWithHTML()
        {
            //Arrange
            var note = process.GetNote("front content", "back content", "blabla");
            var json = JsonConvert.SerializeObject(note);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8765/");

            //Act
            HttpResponseMessage response = await client.PostAsync("", data);

            //Assert - faire un get
        }
    }
}
