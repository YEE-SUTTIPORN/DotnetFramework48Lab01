using DotnetFramework48Lab01.Data;
using DotnetFramework48Lab01.Filters;
using DotnetFramework48Lab01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotnetFramework48Lab01.Controllers
{
    [LogActionFilter]
    public class UserRoleController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            var list = db.UserRoles.ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserRoleModel obj)
        {
            if (ModelState.IsValid)
            {
                db.UserRoles.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}