using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotnetFramework48Lab01.Filters
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = HttpContext.Current;

            if (httpContext.Session["Roles"] == null)
            {
                return false;
            }

            return httpContext.Session["Roles"].ToString() == "Admin";
        }
    }
}