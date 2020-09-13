using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Khun9Room.Data;
using Microsoft.AspNetCore.Mvc;

namespace Khun9Room.Areas.User.Controllers
{
    public class AppUserController : Controller
    {
        ApplicationDbContext _db;
        public AppUserController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult EditProfile(int id)
        {
            var getAppUuser = _db.ApplicationUsers.SingleOrDefault(u => u.Id == id.ToString());

            return View(getAppUuser);
        }
    }
}
