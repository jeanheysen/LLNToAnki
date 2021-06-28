using LLNToAnki.BE.Ports;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LLNToAnki.Infrastructure.AnkiConnecting
{
    public class ConnectNotePoster : IConnectNotePoster
    {
        private HttpClient client;

        public ConnectNotePoster()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8765/");
        }

        public async Task<string> Post(IConnectNote connectNote)
        {
            var json = JsonConvert.SerializeObject(connectNote);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("", data);

            var body = await response.Content.ReadAsStringAsync();

            return body;
        }
    }
}
