﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SpotifyDemo1.Objects;

namespace SpotifyDemo1
{
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
            bool wait = false;
            string responseJson = string.Empty;
            int attempt = 1;

            while(string.IsNullOrEmpty(responseJson) && attempt <= configuration.Settings.RequestSettings.MaxAttempts) 
            {
                if(wait) { 
                    console.WriteLine($"Rate limit reached; Wait and retry {attempt} / {configuration.Settings.RequestSettings.MaxAttempts}");
                    await Task.Delay(5000); 
                }
                using (var httpRequest = new HttpRequestMessage(HttpMethod.Get, url)) 
                {
                    using (var response = await httpClient.SendAsync(httpRequest))
                    {
                        wait = (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests);
                        if(wait) {
                            attempt++;
                            continue;
                        }
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
}
