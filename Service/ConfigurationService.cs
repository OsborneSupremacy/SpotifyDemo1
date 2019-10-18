using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using SpotifyDemo1.Objects;

namespace SpotifyDemo1
{
    public class ConfigurationService
    {
        public string GetUserNameAndPasswordBase64()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            var apiKeys = config.GetSection("apikeys").Get<ApiKeys>();

            var apiKeysBytes = Encoding.ASCII.GetBytes($"{apiKeys.ClientId}:{apiKeys.ClientSecret}");

            return Convert.ToBase64String(apiKeysBytes);
        }
    }
}
