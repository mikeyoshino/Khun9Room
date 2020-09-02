using Khun9Room.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khun9Room.ViewModels
{
    public class PropertyViewModel
    {
        public List<ApplicationUser> ApplicationUsers { get; set; }
        public Property Property { get; set; }
    }
}
