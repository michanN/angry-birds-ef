using System;
using System.Collections.Generic;
using System.Text;

namespace AngryBirds.CLIENT.Models
{
    public class Player
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }

        public ICollection<Round> Rounds { get; set; }
    }
}
