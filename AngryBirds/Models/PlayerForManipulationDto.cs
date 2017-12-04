using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngryBirds.API.Models
{
    public class PlayerForManipulationDto
    {
        public string Name { get; set; }
        public ICollection<RoundForCreationDto> Rounds { get; set; }
    }
}
