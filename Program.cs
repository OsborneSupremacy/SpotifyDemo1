
namespace SpotifyDemo1;

class Program
{
    static void Main(string[] args)
    {
        while (UserSearch()) { }
    }

    static bool UserSearch()
    {
        var config = new ConfigurationService();
        var console = new ConsoleService(config);
        var apirequest = new ApiRequestService(console, config);
        var spotify = new SpotifyService(console, config, apirequest);
        var analysis = new AnalysisService();

        var username = console.PromptForUsername();

        // get all public playlists for the provided username
        var playlists = spotify.GetPlaylists(username, out bool anyFound);
        // if the provided user has no public playlists, ask for another username
        if (!anyFound) return true;

        // get metadata for each public playlist. This will give us a list of tracks.
        var playlistMetadatum = spotify.GetAllPlaylistMetadatum(playlists.items);

        // identify unique tracks from the playlist metadataum
        var tracks = analysis.IdentifyUniqueTracks(playlistMetadatum);

        // identify unique artists from the tracks. This just gets us artist names / IDs
        var artists = analysis.IdentifyUniqueArtists(tracks);

        // get the full artist info, which gets us genres.
        var fullArtists = spotify.GetAllArtists(artists);

        // identify unique genres
        var genres = analysis.IdentifyUniqueGenres(fullArtists);

        var audioFeatures = spotify.GetAudioFeaturesForAllTracks(tracks);

        var avgAudioFeatures = analysis.CalculateAverageAudioFeatures(audioFeatures);

        console.WriteMetrics(
            artists, 
            genres,
            audioFeatures, 
            avgAudioFeatures);

        return true;
    }
}
