using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Khun9Room.Models;

namespace Khun9Room.ViewModels
{
    public class RoomViewModel
    {
        public Room Room { get; set; }
        public IEnumerable<UnitNumber> UnitNumbers { get; set; }
    }
}
