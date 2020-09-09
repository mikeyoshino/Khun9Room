using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Khun9Room.Data;
using Khun9Room.Models;
using Microsoft.AspNetCore.Mvc;

namespace Khun9Room.Areas.User.Controllers
{
    public class UnitNumberController : Controller
    {
        ApplicationDbContext _db;
        public UnitNumberController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            var unit = new UnitNumber();
            return View(unit);
        }

        [HttpPost]
        public IActionResult Add(UnitNumber unit)
        {

            if (ModelState.IsValid)
            {
                if (unit.UnitNumberId == 0)
                {
                    _db.UnitNumbers.Add(unit);
                    unit.IsTaken = false;
                }
                else
                {
                    _db.UnitNumbers.Update(unit);
                }
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public IActionResult MultipleAdd(UnitNumber unit, int rangeFrom, int rangeTo, string unitPrefix)
        {
            for (int i = rangeFrom; i < rangeTo; i++)
            {
                unit.UnitPrefix = unitPrefix;
                unit.UnitNumberId = i;
                unit.IsTaken = false;
                _db.UnitNumbers.Add(unit);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "UnitNumbers");


        }
    }
}
