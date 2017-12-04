using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngryBirds.API.Models
{
    public class RoundDto
    {
        public Guid RoundId { get; set; }
        public Guid PlayerId { get; set; }
        public Guid MapId { get; set; }

        public int Points { get; set; }
        public PlayerDto Player { get; set; }
        public MapDto Map { get; set; }
    }
}
