using System;
using System.Collections.Generic;
using System.Text;

namespace AngryBirds.CLIENT.Models
{
    public class Round
    {
        public Guid RoundId { get; set; }
        public Guid PlayerId { get; set; }
        public Guid MapId { get; set; }

        public int Points { get; set; }
    }
}
