using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Khun9Room.Data;
using Khun9Room.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Khun9Room.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class TenantController : Controller
    {
        ApplicationDbContext _db;
        public TenantController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var tenantList = _db.Tenants.ToList();

            return View(tenantList);
        }

        public IActionResult Create(int? id)
        {
            var tenent = new Tenant();
            if (id == null)
            {
                return View(tenent);
            }

            tenent = _db.Tenants.SingleOrDefault(t => t.TenantId == id);

            if (tenent == null)
            {
                return NotFound();
            }
            return View(tenent);

        }

        [HttpPost]
        public IActionResult Create(Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                if (tenant.TenantId == 0)
                {
                    _db.Tenants.Add(tenant);
                }
                else
                {
                    _db.Tenants.Update(tenant);
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

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var objFromDb = _db.Tenants.SingleOrDefault(t => t.TenantId == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "เกิดข้อผิดพลาดระหว่างการลบ" });
            }
            _db.Tenants.Remove(objFromDb);
            _db.SaveChanges();

            return Json(new { success = true, message = "ดำเนินการลบสำเร็จ" });

        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _db.Tenants.ToList();
            return Json(new { data = allObj });
        }
        #endregion


    }
}
