using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Khun9Room.Models;
using Khun9Room.Data;
using Khun9Room.ViewModels;

namespace Khun9Room.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var roomList = _db.Rooms.ToList();
            var propertyList = _db.Properties.ToList();
            var tenantList = _db.Tenants.ToList();

            var dashboard = new HomeViewModel()
            {
                Rooms = roomList,
                Properties = propertyList,
                Tenants = tenantList
            };

            return View(dashboard);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
