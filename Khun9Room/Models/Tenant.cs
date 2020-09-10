using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Khun9Room.Models
{
    public class Tenant
    {
        [Key]
        public int TenantId { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "ชื่อต้น")]
        public string FirstName { get; set; }

        [Required]
        [Display (Name = "นามสกุล")]
        [MinLength(2)]
        public string LastName { get; set; }

        [Display(Name = "สถานที่เช่า")]
        public int PropertyId { get; set; }
        public ICollection<Room> Rooms { get; set; }

        public ApplicationUser applicationUser { get; set; }
        [ForeignKey("ApplicationId")]
        public string ApplicationUserId { get; set; }

        public bool IsRent { get; set; }
        [NotMapped]
        public string FullName
        {
            get { return string.Format("{0} {1}", this.FirstName, this.LastName); }
        }
    }
}
