using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using whManagerLIB.Models;
using Newtonsoft.Json;

namespace whManagerUI.Classes
{
    public class WorkerApi
    {
        private readonly HttpClient _httpClient;

        public WorkerApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<Worker>> get()
        {
            string requestEndpoint = "workers";
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestEndpoint);
            string json = await httpResponse.Content.ReadAsStringAsync();
            return (JsonConvert.DeserializeObject<IList<Worker>>(json));
        }
    }
}
