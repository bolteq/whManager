using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using whManagerLIB.Models;
using whManagerUI.Classes;

namespace whManagerUI.Pages
{
    public class IndexModel : PageModel
    {
        public IList<Worker> Workers { get; set; }
        private WorkerApi workerApi;
        private readonly HttpClient _httpClient;
        public WorkSchedule WorkSchedule { get; set; }

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            workerApi = new WorkerApi(_httpClient);
        }

        public async Task OnGetAsync()
        {
            Workers = await workerApi.get();
        }

    }
}
