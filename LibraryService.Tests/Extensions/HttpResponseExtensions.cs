using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LibraryService.Tests.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task<T> ReadBody<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
