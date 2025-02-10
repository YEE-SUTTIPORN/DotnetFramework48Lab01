using DotnetFramework48Lab01.Data;
using DotnetFramework48Lab01.Models;
using DotnetFramework48Lab01.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DotnetFramework48Lab01.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            var result = db.Users.ToList();
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserModel obj)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginDto obj)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == obj.Username && u.PasswordHash == obj.Password);
            if (user != null)
            {
                var roles = db.UserRoles.Join(db.Roles, ur => ur.RoleID, r => r.RoleID, (ur, r) => new { ur, r })
                    .Where(x => x.ur.UserID == user.UserID)
                    .Select(x => x.r.RoleName)
                    .ToArray();

                string rolesString = string.Join(",", roles);

                FormsAuthentication.SetAuthCookie(user.Username, false);
                Session["Username"] = user.Username;
                Session["Roles"] = rolesString;

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Invalid Username or Password";
            return View();
        }

        [AllowAnonymous]
        public ActionResult Unauthorized()
        {
            return View();
        }

        // Logout
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}