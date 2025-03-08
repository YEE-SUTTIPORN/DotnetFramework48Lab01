using DotnetFramework48Lab01.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotnetFramework48Lab01.Controllers
{
    [LogActionFilter]
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult SetSection(string section)
        {
            Session["Section"] = section;
            return Redirect(Request.UrlReferrer.ToString()); // Redirect กลับไปยังหน้าเดิม
        }

        public ActionResult Index()
        {
            var section = Session["Section"] as string ?? "Infrastructure";
            ViewBag.Section = section;
            return View();
        }

        [RoleAuthorize("Admin")]
        public ActionResult AdminDashboard()
        {
            return View();
        }

        [RoleAuthorize("Operator")]
        public ActionResult OperatorDashboard()
        {
            return View();
        }

        [RoleAuthorize("Manager")]
        public ActionResult ManagerDashboard()
        {
            return View();
        }

    }
}