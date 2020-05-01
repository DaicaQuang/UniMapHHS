using System.ComponentModel.DataAnnotations;

namespace UniMapHHS.Models
{
    public class Glossary
    {
        [Key]
        public int GlossaryId { get; set; }

        [Required]
        public string Dutch { get; set; }

        [Required]
        public string English { get; set; }

        [Required]
        public string Spanish { get; set; }
    }
}
