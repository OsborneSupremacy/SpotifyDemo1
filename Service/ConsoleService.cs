using SpotifyDemo1.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpotifyDemo1
{
    public class ConsoleService
    {
        public ConfigurationService configuration { get; set; }

        public ConsoleService(ConfigurationService configurationService) {
            configuration = configurationService;
        }

        public string PromptForUsername()
        {
            WriteLine();
            Write("Spotify username: ");
            return Console.ReadLine();
        }

        public void Write(string input) =>
            Console.Write(input);

        public void WriteLine() =>
            Console.WriteLine();

        public void WriteLine(string input) =>
            Console.WriteLine(input);

        public void WriteMetrics(
            List<Artist> uniqueArtists, 
            List<Genre> genres,
            List<AudioFeatures> audioFeatures,
            AudioFeatures avgAudioFeatures)
        {
            WriteLine();
            WriteLine("METRICS:");
            WriteLine("------------------------------------------------------------");
            WriteLine();

            WriteLine("Top artists:");
            WriteLine();
            int i = 0;
            foreach (var artist in uniqueArtists.OrderByDescending(x => x.TrackCount))
            {
                WriteLine($"{++i}. {artist.name} - {artist.TrackCount} tracks");
                if (i >= configuration.Settings.MetricSettings.TopArtistsToShow) break;
            }
            WriteLine();

            WriteLine("Top genres (tracks can have multiple genres):");
            WriteLine();
            int g = 0;
            foreach (var genre in genres.OrderByDescending(x => x.TrackCount))
            {
                WriteLine($"{++g}. {genre.name} - {genre.TrackCount} tracks");
                if (g >= configuration.Settings.MetricSettings.TopGenresToShow) break;
            }
            WriteLine();

            WriteLine();
            WriteLine($"Number of tracks with Spotify Audio Features data: {audioFeatures.Count()}");
            WriteLine();
            WriteLine($"Avg Danceability (0 = least, 1 = most)        : {avgAudioFeatures.danceability}");
            WriteLine($"Avg Energy (0 = least, 1 = most)              : {avgAudioFeatures.energy}");
            WriteLine($"Avg Loudness (dB, range is -60 to 0)          : {avgAudioFeatures.loudness}");
            WriteLine($"Avg Tempo (BPM, range is 0 to 250)            : {avgAudioFeatures.tempo}");
        }

    }
}
