using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using whManagerLIB.Helpers;
using whManagerLIB.Models;

namespace whManagerUI.Services
{
    public class CompanyService
    {
        private readonly HttpClient _httpClient;
        public CompanyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Company> GetCompany(int id, string token)
        {
            string requestEndpoint = $"company/{id}";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await _httpClient.SendAsync(requestMessage);

            if (httpResponse.IsSuccessStatusCode)
            {
                var data = await httpResponse.Content.ReadAsStringAsync();
                var company = JsonConvert.DeserializeObject<Company>(data);

                return company;
            }

            return null;
        }

        public async Task<List<Company>> GetCompanies(string token)
        {
            string requestEndpoint = "company";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await _httpClient.SendAsync(requestMessage);

            if (httpResponse.IsSuccessStatusCode)
            {
                var data = await httpResponse.Content.ReadAsStringAsync();
                var companies = JsonConvert.DeserializeObject<List<Company>>(data);

                return companies;
            }

            return null;
        }

        public async Task<Result> SetCompany(Company company, string token)
        {
            string requestEndpoint = "company";
            var payload = new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestEndpoint);
            requestMessage.Content = payload;
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage httpResponse = await _httpClient.SendAsync(requestMessage);

            if (httpResponse.IsSuccessStatusCode)
            {
                var result = new Result()
                {
                    Message = SuccessMessages.ObjectCreated,
                    Status = true
                };
                return result;
            }
            else
            {
                var result = new Result()
                {
                    Message = Errors.InsertDataFailed,
                    Status = false
                };

                return result;
            }
        }

        public async Task<Result> DeleteCompany(int id, string token)
        {
            string requestEndpoint = $"company?id={id}";

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage httpResponse = await _httpClient.SendAsync(requestMessage);

            if (httpResponse.IsSuccessStatusCode)
            {
                var result = new Result()
                {
                    Message = SuccessMessages.ObjectDeleted,
                    Status = true
                };

                return result;
            }
            else
            {
                var result = new Result()
                {
                    Message = Errors.DeleteDataFailed,
                    Status = false
                };

                return result;
            }
        }

    }
}
