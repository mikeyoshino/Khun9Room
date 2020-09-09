using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Khun9Room.Models
{
    public class UnitNumber
    {
        [Key]
        public int UnitNumberId { get; set; }

        public string UnitPrefix { get; set; }

        public bool IsTaken { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}
