using System;
using System.Collections.Generic;
using System.Text;

namespace AngryBirds.CORE.Models
{
    public class Map
    {
        public Guid MapId { get; set; }

        public string Name { get; set; }

        public int MaxMoves { get; set; }

        public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();
    }
}
