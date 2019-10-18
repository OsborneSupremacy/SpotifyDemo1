using Newtonsoft.Json;
using SpotifyDemo1.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyDemo1
{
    public class SpotifyService
    {
        public ConsoleService console { get; set; }

        public ConfigurationService configuration { get; set; }

        protected HttpClient httpClient { get; set; }

        protected string token { get; set; }

        public SpotifyService(ConsoleService consoleService, ConfigurationService configurationService)
        {
            console = consoleService;
            configuration = configurationService;

            token = GetSpotifyToken().access_token;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public Token GetSpotifyToken()
        {
            var postClient = new HttpClient();
            postClient.DefaultRequestHeaders.Add("Authorization", $"Basic {configuration.GetUserNameAndPasswordBase64()}");

            var url = "https://accounts.spotify.com/api/token";

            return JsonConvert.DeserializeObject<Token>
                (ApiRequestService.PostForm(postClient, url,
                new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("grant_type", "client_credentials") } )
                .GetAwaiter()
                .GetResult());
        }

        public List<AudioFeatures> GetAudioFeaturesForAllTracks(List<Track> tracks)
        {
            var tasks = new List<Task<AudioFeatures>>();

            int i = 0;
            foreach (var track in tracks)
            {
                if (string.IsNullOrEmpty(track.id))
                {
                    Console.WriteLine($"Track `{++i}` has no ID, skipping");
                    continue;
                }
                Console.WriteLine($"Getting audio features for track `{track.id}`, {++i} / {tracks.Count}");
                tasks.Add(GetAudioFeatures(track.id));
            }

            return Task.WhenAll(tasks).GetAwaiter().GetResult().Where(x => x != null).ToList();
        }

        public async Task<AudioFeatures> GetAudioFeatures(string trackID)
        {
            string url = $"https://api.spotify.com/v1/audio-features/{trackID}";
            return JsonConvert.DeserializeObject<AudioFeatures>(await ApiRequestService.Get(httpClient, url));
        }

        public PlaylistSearch GetPlaylists(string username, out bool anyFound)
        {
            var url = $"https://api.spotify.com/v1/users/{username}/playlists";
            var playlists = JsonConvert.DeserializeObject<PlaylistSearch>(ApiRequestService.Get(httpClient, url).GetAwaiter().GetResult());

            anyFound = (playlists?.items?.Any() ?? false);

            if (!anyFound)
                console.WriteLine($"No public playlists found for user `{username}`");
            else
                console.WriteLine($"{playlists?.items?.Count} public playlists found");

            return playlists;
        }

        public List<PlaylistMeta> GetAllPlaylistMetadatum(List<Playlist> playlists)
        {
            var tasks = new List<Task<PlaylistMeta>>();
            foreach (var playlist in playlists)
            {
                Console.WriteLine($"Getting tracks for playlist `{playlist.id}`...");
                tasks.Add(GetPlaylistMeta(playlist.id, 100));
            }
            return Task.WhenAll(tasks).GetAwaiter().GetResult().Where(x => x != null).ToList();
        }

        public async Task<PlaylistMeta> GetPlaylistMeta(string playlistID, int limit)
        {
            string url = $"https://api.spotify.com/v1/playlists/{playlistID}/tracks?limit={limit}";
            return JsonConvert.DeserializeObject<PlaylistMeta>(await ApiRequestService.Get(httpClient, url));
        }
    }
}
