using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using whManagerLIB.Models;


namespace whManagerUI.Pages
{
    public class IndexModel : PageModel
    {
        public IList<Worker> Workers { get; set; }
        public readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public async void OnGet()
        //{
        //    HttpResponseMessage httpResponse = await _httpClient.GetAsync("worker");
        //    string json = await httpResponse.Content.ReadAsStringAsync();
        //    IList<Worker> Workers = JsonConvert.DeserializeObject<IList<Worker>>(json);
        //}

        public async Task OnGetAsync()
        {
            string requestEndpoint = "worker";
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestEndpoint);
            string json = await httpResponse.Content.ReadAsStringAsync();
            Workers = JsonConvert.DeserializeObject<IList<Worker>>(json);
        }

    }
}
