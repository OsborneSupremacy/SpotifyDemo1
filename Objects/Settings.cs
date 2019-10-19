using Newtonsoft.Json;

namespace SpotifyDemo1.Objects
{
    public class Settings {
        public ApiKeys ApiKeys { get; set; }

        public RequestSettings RequestSettings { get; set; }

        public MetricSettings MetricSettings { get; set; }

    }

    [JsonObject("apikeys")]
    public class ApiKeys
    {
        [JsonProperty("clientid")]
        public string ClientId { get; set; }

        [JsonProperty("clientsecret")]
        public string ClientSecret { get; set; }
    }

    [JsonObject("requestSettings")]
    public class RequestSettings
    {
        [JsonProperty("maxAttempts")]
        public int MaxAttempts { get; set; }
    }
    
    [JsonObject("metricSettings")]
    public class MetricSettings
    {
        [JsonProperty("topArtistsToShow")]
        public int TopArtistsToShow { get; set; }

        [JsonProperty("TopGenresToShow")]
        public int TopGenresToShow { get; set; }
    }

}
