using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AngryBirds.DATA.Entities
{
    public class Player
    {
        public Guid PlayerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Round> Rounds { get; set; } = new List<Round>();
    }
}
