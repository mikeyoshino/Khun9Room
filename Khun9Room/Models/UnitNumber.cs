using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Khun9Room.Models
{
    public class UnitNumber
    {
        [Key]
        public int UnitNumberId { get; set; }

        public int UnitNo { get; set; }

        public string UnitPrefix { get; set; }

        public bool IsTaken { get; set; }

        public ICollection<Room> Rooms { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }
    }
}
