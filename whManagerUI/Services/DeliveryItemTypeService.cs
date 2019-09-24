using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using whManagerLIB.Models;
using whManagerLIB.Helpers;
using System.Text;
using System.Net.Http.Headers;
using whManagerUI.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace whManagerUI.Services
{
    public class DeliveryItemTypeService
    {
        private readonly HttpClient _httpClient;

        public DeliveryItemTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        #region GetDeliveryItemType
        public async Task<DeliveryItemType> GetDeliveryItemType(int id, string token)
        {
            string requestEndpoint = $"DeliveryItemType/{id}";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await _httpClient.SendAsync(requestMessage);

            if (httpResponse.IsSuccessStatusCode)
            {
                var data = await httpResponse.Content.ReadAsStringAsync();
                var deliveryItemType = JsonConvert.DeserializeObject<DeliveryItemType>(data);

                return deliveryItemType;
            }

            return null;
        }
        #endregion

        #region GetDeliveryItemTypes
        public async Task<List<DeliveryItemType>> GetDeliveryItemTypes(string token)
        {
            string requestEndpoint = "DeliveryItemType";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await _httpClient.SendAsync(requestMessage);
            if (httpResponse.IsSuccessStatusCode)
            {
                var data = await httpResponse.Content.ReadAsStringAsync();
                var deliveryItemTypes = JsonConvert.DeserializeObject<List<DeliveryItemType>>(data);

                return deliveryItemTypes;
            }

            return null;
        }
        #endregion

        #region SetDeliveryItemType
        public async Task<Result> SetDeliveryItemType(DeliveryItemType deliveryItemType, string token)
        {
            string requestEndpoint = "DeliveryItemType";
            var payload = new StringContent(JsonConvert.SerializeObject(deliveryItemType), Encoding.UTF8, "application/json");

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
        #endregion

        #region DeleteDeliveryItemType
        public async Task<Result> DeleteDeliveryItemType(int id, string token)
        {
            string requestEndpoint = $"DeliveryItemType?id={id}";

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
        #endregion
    }
}
