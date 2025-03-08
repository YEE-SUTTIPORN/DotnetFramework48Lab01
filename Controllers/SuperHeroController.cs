using DotnetFramework48Lab01.Data;
using DotnetFramework48Lab01.Filters;
using DotnetFramework48Lab01.Helpers;
using DotnetFramework48Lab01.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DotnetFramework48Lab01.Controllers
{
    [LogActionFilter]
    public class SuperHeroController : Controller
    {
        private readonly AppDbContext _db = new AppDbContext();

        public SuperHeroController()
        {

        }

        // GET: SuperHero
        public ActionResult Index()
        {
            //try
            //{
            //    throw new Exception("Test Exception");
            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            return View();
        }

        [HttpPost]
        public ActionResult Create(SupperHeroVM obj, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", obj);
            }

            try
            {
                Thread.Sleep(2000);

                obj.Id = 0;
                obj.CreatedDate = DateTime.Now;
                obj.CreatedBy = "System";
                byte[] imageByte = null;
                var supperObj = new SupperHeroModel
                {
                    Image = imageByte,
                    CreatedBy = obj.CreatedBy,
                    CreatedDate = obj.CreatedDate,
                    SuperHeroName = obj.SuperHeroName,
                    SuperHeroPower = obj.SuperHeroPower
                };

                if (Image != null && Image.ContentLength > 0)
                {
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                    string fileExtension = Path.GetExtension(Image.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return Json(new { success = false, message = "อนุญาตให้อัปโหลดเฉพาะไฟล์ .jpg และ .png เท่านั้น" });
                    }


                    imageByte = ImageHelper.ResizeAndConvertToByteArray(Image, 800, 800);
                }

                supperObj.Image = imageByte;

                _db.SuperHeroes.Add(supperObj);

                _db.SaveChanges();
                ModelState.Clear();

                return View("_SuperHeroList", _db.SuperHeroes.ToList());

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult GetSuperHero()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var list = db.SuperHeroes.ToList();
                    return View("_SuperHeroList", list);
                }
            }
            catch (Exception ex)
            {
                var list = _db.SuperHeroes.ToList();
                return View("_SuperHeroList", list);
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
                TempData["ErrorMessage"] = "อนุญาตให้อัปโหลดเฉพาะไฟล์ .jpg และ .png เท่านั้น";
                return View();
            }
        }
    }
}