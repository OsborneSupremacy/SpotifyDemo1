using SpotifyDemo1.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyDemo1
{
    public class AnalysisService
    {
        public List<Track> IdentifyUniqueTracks(List<PlaylistMeta> playlistMetadatum) =>
            playlistMetadatum
                .SelectMany(x => x.items)
                .Select(x => x.track)
                .Distinct()
                .ToList();

        public List<Artist> GetUniqueArtists(List<Track> tracks)
        {
            var uniqueArtists = tracks
                .Where(x => x?.album?.artists != null)
                .SelectMany(x => x.album.artists)
                .Where(x => !string.IsNullOrEmpty(x.name))
                .Distinct()
                .ToList();

            foreach (var artist in uniqueArtists)
                artist.TrackCount =
                    tracks.Where(x => x.album.artists.Select(a => a.id).Contains(artist.id)).Count();

            return uniqueArtists;
        }

        public AudioFeatures CalculateAverageAudioFeatures(List<AudioFeatures> audioFeatures) =>
            new AudioFeatures()
            {
                danceability = audioFeatures.Select(x => x.danceability).Average(),
                energy = audioFeatures.Select(x => x.energy).Average(),
                loudness = audioFeatures.Select(x => x.loudness).Average(),
                tempo = audioFeatures.Select(x => x.tempo).Average()
            };

    }
}
