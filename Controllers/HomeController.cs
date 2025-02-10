using DotnetFramework48Lab01.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotnetFramework48Lab01.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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