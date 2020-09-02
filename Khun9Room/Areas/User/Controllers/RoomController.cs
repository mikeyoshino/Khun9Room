using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Khun9Room.Data;
using Khun9Room.Models;
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
            var room = new Room();
            if (id == null)
            {
                return View(room);
            }

            room = _db.Rooms.SingleOrDefault(t => t.RoomNumber == id);

            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        public IActionResult Add(Room room)
        {
            if (ModelState.IsValid)
            {
                if (room.RoomNumber == 0)
                {
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
        public IActionResult GetTenantList(int id)
        {

            var tenantList = _db.Tenants.Where(t => t.PropertyId == id).ToList();

            return new JsonResult(tenantList);

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var objFromDb = _db.Rooms.SingleOrDefault(t => t.RoomNumber == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "เกิดข้อผิดพลาดระหว่างการลบ" });
            }
            _db.Rooms.Remove(objFromDb);
            _db.SaveChanges();

            return Json(new { success = true, message = "ดำเนินการลบสำเร็จ" });

        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _db.Rooms.Include(t => t.Tenant).ToList();
            
            return Json(new { data = allObj });
        }
        #endregion
    }
}
