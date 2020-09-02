using Khun9Room.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khun9Room.Models
{
    public class Unit
    {
        ApplicationDbContext _db;
        public Unit(ApplicationDbContext db)
        {
            _db = db;
        }


        public int UnitId { get; set; }
        public string UnitPrefix { get; set; }
        public int UnitNumber { get; set; }


        public static void AutoGenetateUnit(Unit unit, int numberOfUnit)
        {
            for (int i = 0; i < numberOfUnit; i++)
            {
            }
        }


    }
}
