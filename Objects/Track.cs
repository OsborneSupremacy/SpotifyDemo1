using System;

namespace SpotifyDemo1.Objects;

public class Track : IEquatable<Track>
{
    public string id { get; set; }

    public string name { get; set; }

    public int? popularity { get; set; }

    public Album album { get; set; }

    public AudioFeatures audioFeatures { get; set; }

    public bool Equals(Track other) =>
        (id ?? "0").Equals(other.id ?? "0");

    public override int GetHashCode() =>
        (id ?? "0").GetHashCode();
}
