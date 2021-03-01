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
        private Process process;

        public S003_AnkiConnectTests()
        {
            process = new Process();
        }

        [Test]
        public async Task T001_AddNotetoAnkiAndRetrieveIt()
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
        public void T002_BuildContentWithJsonConverterReturnsSameResultIgnoringCase()
        {
            //Arrange
            var note = process.GetNote();

            //Act
            var json = JsonConvert.SerializeObject(note).ToLower();

            //Assert
            StringAssert.AreEqualIgnoringCase(process.GetJsonContent(), json);
        }

        [Test]
        public void T003_BuildContentWithJsonConverterReturnsSameResultWithCase()
        {
            //Arrange
            var note = process.GetNote();

            //Act
            var json = JsonConvert.SerializeObject(note);

            //Assert
            Assert.AreEqual(process.GetJsonContent(), json);
        }

        [Test]
        public async Task T004_WorksWithBuildNoteConnectObject()
        {
            //Arrange
            var note = process.GetNote();
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
