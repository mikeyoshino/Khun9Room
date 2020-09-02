using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Khun9Room.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PropertyName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public int PostCode { get; set; }

        public ApplicationUser applicationUser { get; set; }
        [ForeignKey("ApplicationId")]
        public string applicationUserId { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}
