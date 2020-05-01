using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//test2

namespace UniMapHHS.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        public int MaxCapacity { get; set; }

        [Required]
        public string Title { get; set; }

        public bool Locked { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("BuildingFloor")]
        public int BuildingFloorId { get; set; }
        public BuildingFloor BuildingFloor { get; set; }

        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<History> Histories { get; set; }
    }
}
