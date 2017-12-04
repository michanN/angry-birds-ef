using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AngryBirds.CORE.Models
{
    public class Round
    {
        public Guid RoundId { get; set; }
        public Guid PlayerId { get; set; }
        public Guid MapId { get; set; }

        public int Points { get; set; }

        public Player Player { get; set; }

        public Map Map { get; set; }
    }
}
