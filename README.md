1. 

Kod:
        public async Task OnGetAsync()
        {
            string requestEndpoint = "worker";
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestEndpoint);
            string json = await httpResponse.Content.ReadAsStringAsync();
            Workers = JsonConvert.DeserializeObject<IList<Worker>>(json);
        }


Domy�lnie by�o OnGet(){}
Czy mo�na to nazywa� jak chc�? Od czego zale�y ? Mog�oby by� OnGetDupa() i by si� wykona�o?

2. 

            HttpClient httpClient = new HttpClient() {
                BaseAddress = Configuration.GetValue<Uri>("API")
            };
            ServicePointManager.FindServicePoint(httpClient.BaseAddress).ConnectionLeaseTimeout = 60000; // sixty seconds

            services.AddSingleton<HttpClient>(httpClient);

???