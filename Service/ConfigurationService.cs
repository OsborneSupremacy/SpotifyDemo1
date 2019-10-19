using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using SpotifyDemo1.Objects;

namespace SpotifyDemo1
{
    public class ConfigurationService
    {
        public Settings Settings { get; private set; }

        public ConfigurationService() 
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            Settings = config.Get<Settings>();
        }

        public string GetUserNameAndPasswordBase64()
        {
            var apiKeysBytes = Encoding.ASCII.GetBytes($"{Settings.ApiKeys.ClientId}:{Settings.ApiKeys.ClientSecret}");
            return Convert.ToBase64String(apiKeysBytes);
        }
    }
}
