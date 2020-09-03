using Khun9Room.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Khun9Room.Models
{
    public class Unit
    {
        [Key]
        public int UnitId { get; set; }
        public string UnitPrefix { get; set; }
        public int UnitNumber { get; set; }


        public bool IsTaken { get; set; }




    }
}
