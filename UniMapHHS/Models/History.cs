using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniMapHHS.Models
{
    public class History
    {
        [Key]
        public int HistoryId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
