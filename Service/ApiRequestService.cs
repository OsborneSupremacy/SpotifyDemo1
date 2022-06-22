using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace SpotifyDemo1;

public class ApiRequestService
{
    public ConsoleService console { get; set; }

    public ConfigurationService configuration { get; set; }

    public ApiRequestService(ConsoleService consoleService, ConfigurationService configurationService) {
        console = consoleService;
        configuration = configurationService;
    }

    public async Task<string> Get(HttpClient httpClient, string url)
    {

        string responseJson = string.Empty;
        int attempt = 1;

        while(string.IsNullOrEmpty(responseJson) && attempt <= configuration.Settings.RequestSettings.MaxAttempts) 
        {
            using (var httpRequest = new HttpRequestMessage(HttpMethod.Get, url)) 
            {
                using (var response = await httpClient.SendAsync(httpRequest))
                {
                    var wait = (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests);
                    if(wait) {
                        var waitMs = response.Headers.RetryAfter.Delta.Value.TotalMilliseconds;
                        attempt++;
                        console.WriteLine($"Rate limit exceeded; Try again in {waitMs} ms; Attempt {attempt} / {configuration.Settings.RequestSettings.MaxAttempts}");
                        await Task.Delay((int)waitMs);
                        continue;
                    }
                    // if Spotify returns a 404, no need to keep trying
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return string.Empty;
                    response.EnsureSuccessStatusCode();
                    responseJson = await response.Content.ReadAsStringAsync();
                }
            }
        }
        return responseJson;
    }

    public async Task<string> PostForm(HttpClient httpClient, string url, List<KeyValuePair<string, string>> nameValueList)
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
