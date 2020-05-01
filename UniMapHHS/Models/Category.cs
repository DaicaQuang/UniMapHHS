using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniMapHHS.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [ForeignKey("Glossary")]
        public int Title { get; set; }
        public Glossary Glossary { get; set; }

        [Required]
        public string Icon { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
