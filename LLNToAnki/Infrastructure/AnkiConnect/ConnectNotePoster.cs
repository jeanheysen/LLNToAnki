using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LLNToAnki.Infrastructure.AnkiConnect
{
    public class ConnectNotePoster
    {
        private HttpClient client;

        public ConnectNotePoster()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8765/");
        }

        public async Task<bool> Post(connectNote connectNote)
        {
            var json = JsonConvert.SerializeObject(connectNote);
            
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await client.PostAsync("", data);
            
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
