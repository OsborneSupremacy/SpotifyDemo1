using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyDemo1.Objects;

public class AudioFeatures
{
    public decimal danceability { get; set; }

    public decimal energy { get; set; }

    public int key { get; set; }

    public decimal loudness { get; set; }

    public decimal speechiness { get; set; }

    public decimal acousticness { get; set; }

    public decimal instrumentalness { get; set; }

    public decimal liveness { get; set; }

    public decimal valence { get; set; }

    public decimal tempo { get; set; }

    public int duration_ms { get; set; }

    public int time_signature { get; set; }

    public string id { get; set; }
}
