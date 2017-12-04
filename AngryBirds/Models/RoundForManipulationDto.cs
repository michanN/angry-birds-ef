using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngryBirds.API.Models
{
    public class RoundForManipulationDto
    {
        public int Points { get; set; }
        public PlayerDto Player { get; set; }
        public MapDto Map { get; set; }
    }
}
