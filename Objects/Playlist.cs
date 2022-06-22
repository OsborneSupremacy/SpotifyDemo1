using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyDemo1.Objects;

public class Playlist
{
    public string id { get; set; }

    public string name { get; set; }

    public TrackMeta tracks { get; set; }
}
