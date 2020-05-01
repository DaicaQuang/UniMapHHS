using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniMapHHS.Models
{
    public class Favourite
    {
        [Key, Column(Order = 1), ForeignKey("User")]
        public string Username { get; set; }
        public User User { get; set; }

        [Key, Column(Order = 2), ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }

        
    }
}
