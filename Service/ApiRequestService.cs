using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyDemo1
{
    public class ApiRequestService
    {
        public static async Task<string> Get(HttpClient httpClient, string url)
        {
            using (var httpRequest = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await httpClient.SendAsync(httpRequest))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return string.Empty;
                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    await Task.Delay(5000);
                    return string.Empty;
                }
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
        public static async Task<string> PostForm(HttpClient httpClient, string url, List<KeyValuePair<string, string>> nameValueList)
        {
            using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, url))
            {
                httpRequest.Content = new FormUrlEncodedContent(nameValueList);
                using (var response = await httpClient.SendAsync(httpRequest))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
