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
    class S003_AnkiConnectTests : BaseIntegrationTesting
    {
        private ConnectNoteBuilder connectNoteBuilder;
        private ConnectNotePoster connectNotePoster;
        private HttpClient client;

        public S003_AnkiConnectTests()
        {
            connectNoteBuilder = new ConnectNoteBuilder();
            connectNotePoster = new ConnectNotePoster();
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8765/");
        }

        public string GetJsonContent()
        {
            string content = "{\"action\":\"addNote\",\"version\":6,\"params\":{\"note\":{\"deckName\":\"All\",\"modelName\":\"Full_Recto_verso_before_after_Audio\",\"fields\":{\"Question\":\"front content\",\"Answer\":\"back content\",\"After\":\"blabla\",\"Source\":\"\",\"Audio\":\"\"},\"options\":{\"allowDuplicate\":false,\"duplicateScope\":\"deck\",\"duplicateScopeOptions\":{\"deckName\":\"All\",\"checkChildren\":false}},\"picture\":[{\"url\":\"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/A_black_cat_named_Tilly.jpg/220px-A_black_cat_named_Tilly.jpg\",\"filename\":\"black_cat.jpg\",\"skipHash\":\"8d6e4646dfae812bf39651b59d7429ce\",\"fields\":[\"Back\"]}]}}}";
            return content;
        }


        [Test]
        public void T001_BuildContentWithJsonConverterReturnsSameResultWithCase()
        {
            //Arrange
            var ankiNoteMock = new Mock<IAnkiNote>() { DefaultValue = DefaultValue.Mock };
            ankiNoteMock.SetupGet(a => a.Question).Returns("front content");
            ankiNoteMock.SetupGet(a => a.Answer).Returns("back content");
            ankiNoteMock.SetupGet(a => a.After).Returns("blabla");
            ankiNoteMock.SetupGet(a => a.Audio).Returns("");
            ankiNoteMock.SetupGet(a => a.Source).Returns("");
            var note = connectNoteBuilder.Build(ankiNoteMock.Object);

            //Act
            var json = JsonConvert.SerializeObject(note);

            //Assert
            Assert.AreEqual(GetJsonContent(), json);
        }
    }
}
