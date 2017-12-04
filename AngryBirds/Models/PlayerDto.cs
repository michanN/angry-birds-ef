using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngryBirds.API.Models
{
    public class PlayerDto
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        //public int NumberOfRoundsPlayed { get; set; }

        //public ICollection<RoundForCreationDto> Rounds { get; set; }
    }
}
