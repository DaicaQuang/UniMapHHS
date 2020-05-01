using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniMapHHS.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<Favourite> favourites { get; set; }

        public string langPref { get; set; }
    }
}
