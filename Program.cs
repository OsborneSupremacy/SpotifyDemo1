
namespace SpotifyDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (UserSearch()) { }
        }

        static bool UserSearch()
        {
            var config = new ConfigurationService();
            var console = new ConsoleService();
            var apirequest = new ApiRequestService(console);
            var spotify = new SpotifyService(console, config, apirequest);
            var analysis = new AnalysisService();

            var username = console.PromptForUsername();

            var playlists = spotify.GetPlaylists(username, out bool anyFound);
            if (!anyFound) return true;

            var playlistMetadatum = spotify.GetAllPlaylistMetadatum(playlists.items);

            var tracks = analysis.IdentifyUniqueTracks(playlistMetadatum);

            var audioFeatures = spotify.GetAudioFeaturesForAllTracks(tracks);

            var avgAudioFeatures = analysis.CalculateAverageAudioFeatures(audioFeatures);

            console.WriteMetrics(
                analysis.GetUniqueArtists(tracks), 
                audioFeatures, 
                avgAudioFeatures);

            return true;
        }
    }
}
