using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using whManagerLIB.Models;
using Newtonsoft.Json;
using System.Text;

namespace whManagerUI.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<User> Login(User user)
        {
            string requestEndpoint = "user/login";
            var payload = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync(requestEndpoint, payload);

            var data = await httpResponse.Content.ReadAsStringAsync();
            user = JsonConvert.DeserializeObject<User>(data);

            return user;
        }
    }
}
