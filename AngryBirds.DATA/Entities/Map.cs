using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AngryBirds.DATA.Entities
{
    public class Map
    {
        public Guid MapId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int MaxMoves { get; set; }

        public ICollection<Round> Rounds { get; set; } = new List<Round>();
    }
}
