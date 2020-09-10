using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Khun9Room.Models;

namespace Khun9Room.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Room> Rooms { get; set; }
        public IEnumerable<Tenant> Tenants { get; set; }
        public IEnumerable<Property> Properties { get; set; }
    }
}
