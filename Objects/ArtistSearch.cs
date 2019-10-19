using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyDemo1.Objects
{
    public class ArtistSearch
    {
        public Artists artists { get; set; }

        public class Artists
        {
            public List<Artist> items { get; set; }
        }
    }
}
