using CloudApper.Common.Models;
using CloudApper.Enumerations;
using CloudApper.Model;
using DemoConnector.IServices;
using DemoConnector.ServiceAttributes;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DemoConnector.Services
{
    /// <summary>
    /// All possible new operations handler
    /// </summary>
    /// 
    [ScopedService]
    public class EmailSenderService : IEmailSenderService
    {
        private readonly string _apiURL = Environment.GetEnvironmentVariable(IntegrationConstant.DevelopmentEnvironment.API_BASE_URL);

        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Initialize _httpClientFactory
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public EmailSenderService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Save news
        /// </summary>
        /// <param name="connectorSpec"></param>
        /// <returns></returns>
        public async Task<ResponseMessage<object>> SendEmailAsync(FormPlugin connectorSpec)
        {
            //dummy request model
            object model = new
            {
                Id = Guid.NewGuid(),
                Title = string.Empty,
            };
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiURL); //Or your service url
            client.DefaultRequestHeaders.Add("Accept", "application/json; charset=utf-8");
            HttpResponseMessage response = await client.PostAsJsonAsync("/send-email", model); //example
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsAsync<dynamic>();
                return JsonConvert.DeserializeObject<ResponseMessage<object>>(content);
            }
            return ResponseMsg<dynamic>.GetResponseMessage(response.ReasonPhrase, EnumMessageType.Error, (int)response.StatusCode, null);
        }
    }
}
