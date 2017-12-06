using System;
using System.Collections.Generic;
using System.Text;

namespace AngryBirds.CLIENT.Models
{
    public class Map
    {
        public Guid MapId { get; set; }
        public string Name { get; set; }
        public int MaxMoves { get; set; }

        public ICollection<Round> Rounds { get; set; }
    }
}
