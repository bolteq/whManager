1. 

Kod:
        public async Task OnGetAsync()
        {
            string requestEndpoint = "worker";
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestEndpoint);
            string json = await httpResponse.Content.ReadAsStringAsync();
            Workers = JsonConvert.DeserializeObject<IList<Worker>>(json);
        }


Domyœlnie by³o OnGet(){}
Czy mo¿na to nazywaæ jak chcê? Od czego zale¿y ? Mog³oby byæ OnGetDupa() i by siê wykona³o?

2. 

            HttpClient httpClient = new HttpClient() {
                BaseAddress = Configuration.GetValue<Uri>("API")
            };
            ServicePointManager.FindServicePoint(httpClient.BaseAddress).ConnectionLeaseTimeout = 60000; // sixty seconds

            services.AddSingleton<HttpClient>(httpClient);

???