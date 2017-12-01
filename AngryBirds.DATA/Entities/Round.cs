using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AngryBirds.DATA.Entities
{
    public class Round
    {
        public Guid RoundId { get; set; }

        public Guid PlayerId { get; set; }
        public Guid MapId { get; set; }

        //public int Points { get; set; }

        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        [ForeignKey("MapId")]
        public Map Map { get; set; }
    }
}
