using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpotifyDemo1.Objects
{
    public class Artist : IEquatable<Artist>
    {
        public string id { get; set; }

        public string name { get; set; }

        public List<string> genres { get; set; }

        [JsonIgnore]
        public int TrackCount { get; set; }

        public bool Equals(Artist other) =>
            (id ?? "0").Equals(other.id ?? "0");

        public override int GetHashCode() =>
            (id ?? "0").GetHashCode();
    }
}