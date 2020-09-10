using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Khun9Room.Data;
using Khun9Room.Utility;
using Khun9Room.Models;
using Khun9Room.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Khun9Room.Areas.User.Controllers
{
    [Area("User")]
    public class RoomController : Controller
    {
        ApplicationDbContext _db;
        public RoomController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add(int? id)
        {
            var room = new RoomViewModel()
            {
                Room = new Room(),
                UnitNumbers = _db.UnitNumbers.Where(u => u.IsTaken == false).ToList()

            };

            if (id == null)
            {
                return View(room);
            }

            room.Room = _db.Rooms.SingleOrDefault(t => t.RoomNumber == id);

            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpPost]
        public IActionResult Add(Room room)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var getTenant = _db.Tenants.FirstOrDefault(u => u.TenantId == room.TenantId);
            var getRoom = _db.UnitNumbers.FirstOrDefault(r => r.UnitNumberId == room.UnitNumberId);

            if (ModelState.IsValid)
            {
                if (room.RoomNumber == 0)
                {
                    room.PaymentStatus = PaymentStatus.Paid;
                    room.NextPayDate = DateTime.Today;
                    room.Paydate = DateTime.Today.AddDays(30);
                    room.ApplicationUserId = claims.Value;
                    getTenant.IsRent = true;
                    getRoom.IsTaken = true;
                    _db.Rooms.Add(room);
                    
                }
                else
                {
                    _db.Rooms.Update(room);
                }
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult GetPropertyDropdownList()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var propertyList = _db.Properties.Where(p => p.applicationUserId == claims.Value).Select(n => new { n.Id, n.PropertyName }).ToList();

            return new JsonResult(propertyList);

        }

        [HttpGet]
        public IActionResult GetUnitDropdownList()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var unitList = _db.UnitNumbers.Where(p => p.ApplicationUserId == claims.Value 
                            && p.IsTaken == false)
                            .Select(n => new { n.UnitNumberId, n.UnitNo })
                            .ToList();

            return new JsonResult(unitList);

        }

        [HttpGet]
        public IActionResult GetTenantList(int id)
        {

            var tenantList = _db.Tenants.Where(t => t.PropertyId == id && t.IsRent == false).ToList();

            return new JsonResult(tenantList);

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var objFromDb = _db.Rooms.SingleOrDefault(t => t.RoomNumber == id);
            var getTenant = _db.Tenants.FirstOrDefault(u => u.TenantId == objFromDb.TenantId);
            var getRoom = _db.UnitNumbers.FirstOrDefault(r => r.UnitNumberId == objFromDb.UnitNumberId);

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "เกิดข้อผิดพลาดระหว่างการลบ" });
            }

            getTenant.IsRent = false;
            getRoom.IsTaken = false;

            _db.Rooms.Remove(objFromDb);
            _db.SaveChanges();

            return Json(new { success = true, message = "ดำเนินการลบสำเร็จ" });

        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var allObj = _db.Rooms.Include(t => t.Tenant).Include(t => t.UnitNumber).Where(u=>u.ApplicationUserId == claims.Value).ToList();
            
            return Json(new { data = allObj });
        }

        public IActionResult GetUnitList()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var allObj = _db.UnitNumbers.Where(u => u.ApplicationUserId == claims.Value).ToList();

            return Json(new { data = allObj });
        }
        #endregion


        public IActionResult UnitList()
        {
            return View();
        }


        public IActionResult AddUnit()
        {
            var unit = new UnitNumber();
            return View(unit);
        }

        [HttpPost]
        public IActionResult AddUnit(UnitNumber unitNumber)
        {

            if (ModelState.IsValid)
            {
                if (unitNumber.UnitNumberId == 0)
                {
                    var claimIdentity = (ClaimsIdentity)User.Identity;
                    var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

                    unitNumber.ApplicationUserId = claims.Value;
                    _db.UnitNumbers.Add(unitNumber);
                    unitNumber.IsTaken = false;
                }
                else
                {
                    _db.UnitNumbers.Update(unitNumber);
                }
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
