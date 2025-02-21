using DotnetFramework48Lab01.Data;
using DotnetFramework48Lab01.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DotnetFramework48Lab01.Controllers
{
    public class SuperHeroController : Controller
    {
        private readonly AppDbContext _db = new AppDbContext();

        public SuperHeroController()
        {

        }

        // GET: SuperHero
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult Create(SupperHeroModel obj)
        {
            try
            {
                Thread.Sleep(2000);

                obj.Id = 0;
                obj.CreatedDate = DateTime.Now;
                obj.CreatedBy = "System";

                _db.SuperHeroes.Add(obj);
                _db.SaveChanges();
                ModelState.Clear();

                var list = _db.SuperHeroes.ToList();
                return GetSuperHero();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var list = _db.SuperHeroes.ToList();
                return GetSuperHero();
            }
        }

        public PartialViewResult GetSuperHero()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var list = db.SuperHeroes.ToList();
                    return PartialView("_SuperHeroList", list);
                }
            }
            catch (Exception ex)
            {
                var list = _db.SuperHeroes.ToList();
                return PartialView("_SuperHeroList", list);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var obj = db.SuperHeroes.Find(id);
                    db.SuperHeroes.Remove(obj);
                    db.SaveChanges();
                    return PartialView("_SuperHeroList", db.SuperHeroes.ToList());
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}