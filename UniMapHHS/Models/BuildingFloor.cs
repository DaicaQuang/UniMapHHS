using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniMapHHS.Models
{
    public class BuildingFloor
    {
        [Key]
        public int BuildingFloorId { get; set; }

        [Required]
        public string Building { get; set; }

        [Required]
        public int Floor { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
