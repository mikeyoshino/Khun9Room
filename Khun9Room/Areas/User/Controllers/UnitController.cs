using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Khun9Room.Data;
using Khun9Room.Models;
using Microsoft.AspNetCore.Mvc;

namespace Khun9Room.Areas.User.Controllers
{
    public class UnitController : Controller
    {
        ApplicationDbContext _db;
        public UnitController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            var unit = new Unit();
            return View(unit);
        }

        [HttpPost]
        public IActionResult Add(Unit unit)
        {

            if (ModelState.IsValid)
            {
                if (unit.UnitId == 0)
                {
                    _db.Units.Add(unit);
                    unit.IsTaken = false;
                }
                else
                {
                    _db.Units.Update(unit);
                }
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public IActionResult MultipleAdd(Unit unit, int rangeFrom, int rangeTo, string unitPrefix)
        {
            for (int i = rangeFrom; i < rangeTo; i++)
            {
                unit.UnitPrefix = unitPrefix;
                unit.UnitNumber = i;
                unit.IsTaken = false;
                _db.Units.Add(unit);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "Unit");


        }
    }
}
