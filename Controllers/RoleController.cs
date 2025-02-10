using DotnetFramework48Lab01.Data;
using DotnetFramework48Lab01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotnetFramework48Lab01.Controllers
{
    public class RoleController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            var result = db.Roles.ToList();
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RoleModel obj)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}