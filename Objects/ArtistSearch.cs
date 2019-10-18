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
            public List<artist> items { get; set; }

            public class artist
            {
                public string id { get; set; }

                public string name { get; set; }
            }

        }
    }
}
