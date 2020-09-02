﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Khun9Room.Models
{
    public class Room
    {
        [Key]
        public int RoomNumber { get; set; }

        public int UnitNumber { get; set; }
        public Unit Unit { get; set; }

        [Required]
        public string UnitNumber { get; set; }


        [Required]
        public DateTime MovieIn { get; set; }
        public DateTime? EndLease { get; set; }


        [Required]
        public double RentPrice { get; set; }

        public Property Property { get; set; }
        [ForeignKey("PropertyId")]
        public int PropertyId { get; set; }

        public Tenant Tenant { get; set; }
        [ForeignKey("TenantId")]
        public int TenantId { get; set; }



    }
}