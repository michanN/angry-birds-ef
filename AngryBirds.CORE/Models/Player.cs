using System;
using System.Collections.Generic;
using System.Text;

namespace AngryBirds.CORE.Models
{
    public class Player
    {
        public Guid PlayerId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();
    }
}
