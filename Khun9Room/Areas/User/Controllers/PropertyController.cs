using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Khun9Room.Data;
using Khun9Room.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Khun9Room.Areas.User.Controllers
{
    [Area("User")]
    public class PropertyController : Controller
    {
        ApplicationDbContext _db;
        public PropertyController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            var property = new Property();
            return View(property);
        }

        [HttpPost]
        public IActionResult Add(Property property)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                var EmptyProperty = new Property();
                return View(EmptyProperty);
            }
            property.applicationUserId = claims.Value;

            _db.Properties.Add(property);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {

            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var allObj = _db.Properties.Where(u => u.applicationUserId == claims.Value).ToList();
            return Json(new { data = allObj });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var objFromDb = _db.Properties.SingleOrDefault(t => t.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "เกิดข้อผิดพลาดระหว่างการลบ" });
            }
            _db.Properties.Remove(objFromDb);
            _db.SaveChanges();

            return Json(new { success = true, message = "ดำเนินการลบสำเร็จ" });

        }
        #endregion
    }
}
