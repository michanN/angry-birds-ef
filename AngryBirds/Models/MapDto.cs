using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngryBirds.API.Models
{
    public class MapDto
    {
        public Guid MapId { get; set; }
        public string Name { get; set; }
        public int MaxMoves { get; set; }

        //public ICollection<RoundForCreationDto> Rounds { get; set; }
    }
}
