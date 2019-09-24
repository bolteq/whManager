using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using whManagerLIB.Models;
using Newtonsoft.Json;
using System.Text;
using whManagerLIB.Helpers;
using System.Net.Http.Headers;

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

        public async Task<Result> Register(User user, string token)
        {
            string requestEndpoint = "user/register";
            var payload = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestEndpoint);
            requestMessage.Content = payload;
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage httpResponse = await _httpClient.SendAsync(requestMessage);

            if (httpResponse.IsSuccessStatusCode)
            {
                var result = new Result()
                {
                    Status = true,
                    Message = SuccessMessages.ObjectCreated
                };

                return result;
            }
            else
            {
                var result = new Result()
                {
                    Status = false,
                    Message = Errors.InsertDataFailed
                };

                return result;
            }
            
        }
    }
}
