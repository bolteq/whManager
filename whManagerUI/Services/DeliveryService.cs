using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using whManagerLIB.Models;
using whManagerLIB.Helpers;
using System.Text;
using System.Net.Http.Headers;
using whManagerUI.Helpers;

namespace whManagerUI.Services
{
    public class DeliveryService
    {
        private readonly HttpClient _httpClient;

        public DeliveryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Delivery> GetDelivery(int id, string token)
        {
            string requestEndpoint = $"delivery/{id}";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await _httpClient.SendAsync(requestMessage);

            if (httpResponse.IsSuccessStatusCode)
            {
                var data = await httpResponse.Content.ReadAsStringAsync();
                var delivery = JsonConvert.DeserializeObject<Delivery>(data);

                return delivery;
            }

            return null;
        }
        public async Task<List<Delivery>> GetDeliveries(string token)
        {
            string requestEndpoint = "delivery";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await _httpClient.SendAsync(requestMessage);
            if (httpResponse.IsSuccessStatusCode)
            {
                var data = await httpResponse.Content.ReadAsStringAsync();
                var deliveries = JsonConvert.DeserializeObject<ICollection<Delivery>>(data).ToList();

                return deliveries;
            }

            return null;
        }

        public async Task<bool> AddDelivery(Delivery delivery, string token)
        {
            string requestEndpoint = "delivery";
            var payload = new StringContent(JsonConvert.SerializeObject(delivery), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestEndpoint);
            requestMessage.Content = payload;
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage httpResponse = await _httpClient.SendAsync(requestMessage);

            if (httpResponse.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> DeleteDelivery(int id, string token)
        {
            string requestEndpoint = $"delivery?id={id}";

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage httpResponse = await _httpClient.SendAsync(requestMessage);

            if (httpResponse.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
