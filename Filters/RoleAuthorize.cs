using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotnetFramework48Lab01.Filters
{
    public class RoleAuthorize : AuthorizeAttribute
    {
        private readonly string[] allowedRoles;

        public RoleAuthorize(params string[] roles)
        {
            this.allowedRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["Roles"] == null)
            {
                return false; // ป้องกันการเข้าถึงถ้าไม่มี Role
            }

            // 🚀 ตรวจสอบ Role ของผู้ใช้
            if (allowedRoles.Length > 0)
            {
                var userRoles = httpContext.Session["Roles"].ToString().Split(',');
                return allowedRoles.Any(role => userRoles.Contains(role));
            }

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["Username"] == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Account/Unauthorized");
            }

        }

    }
}