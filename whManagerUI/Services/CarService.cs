using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using whManagerLIB.Models;
using System.Collections.Generic;
using whManagerLIB.Helpers;
using System.Text;
using System.Net.Http.Headers;
using whManagerUI.Helpers;

namespace whManagerUI.Services
{
    public class CarService
    {
        private readonly HttpClient _httpClient;

        public CarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Car> GetCar(int id, SessionHelper sessionHelper)
        {
            string requestEndpoint = $"car/{id}";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", sessionHelper.TokenValue);
            var httpResponse = await _httpClient.SendAsync(requestMessage);

            if (httpResponse.IsSuccessStatusCode)
            {
                var data = await httpResponse.Content.ReadAsStringAsync();
                var car = JsonConvert.DeserializeObject<Car>(data);

                return car;
            }

            return null;
        }
        public async Task<List<Car>> GetCars(SessionHelper sessionHelper)
        {
            string requestEndpoint = "car";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", sessionHelper.TokenValue);
            var httpResponse = await _httpClient.SendAsync(requestMessage);
            if (httpResponse.IsSuccessStatusCode)
            {
                var data = await httpResponse.Content.ReadAsStringAsync();
                var cars = JsonConvert.DeserializeObject<ICollection<Car>>(data).ToList();

                return cars;
            }

            return null;
        }

        public async Task<Result> AddCar(Car car, SessionHelper sessionHelper)
        {
            string requestEndpoint = "car";
            var payload = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestEndpoint);
            requestMessage.Content = payload;
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", sessionHelper.TokenValue);

            HttpResponseMessage httpResponse = await _httpClient.SendAsync(requestMessage);

            if(httpResponse.IsSuccessStatusCode)
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

        public async Task<Result> DeleteCar(int id, SessionHelper sessionHelper)
        {
            string requestEndpoint = $"car?id={id}";

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", sessionHelper.TokenValue);

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
