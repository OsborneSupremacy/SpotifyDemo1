﻿using System.Collections.Generic;

namespace SpotifyDemo1.Objects;

public class Album
{
    public string id { get; set; }

    public string name { get; set; }

    public string release_date { get; set; }

    public List<Artist> artists { get; set; }
}
