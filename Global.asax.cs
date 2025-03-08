using DotnetFramework48Lab01.Data;
using DotnetFramework48Lab01.Helpers;
using DotnetFramework48Lab01.Models;
using Hangfire;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Xml.Linq;

[assembly: OwinStartup(typeof(DotnetFramework48Lab01.Startup))] // แก้ MyNamespace ให้ตรงกับโปรเจกต์ของคุณ


namespace DotnetFramework48Lab01
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // ตั้งค่า Hangfire ให้ใช้ SQL Server Storage
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            RecurringJob.AddOrUpdate("Day-Shift", () => GenerateSuperman(), Cron.Daily(14, 34), new RecurringJobOptions { TimeZone = timeZone });
            RecurringJob.AddOrUpdate("Night-Shift", () => GenerateThor(), Cron.Daily(14, 35), new RecurringJobOptions { TimeZone = timeZone });
            RecurringJob.AddOrUpdate("Night-Shift2", () => GenerateHulk(), Cron.Daily(14, 36), new RecurringJobOptions { TimeZone = timeZone });
        }

        public string GenerateSuperman()
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.SuperHeroes.Add(new SupperHeroModel
                {
                    CreatedBy = "Auto Generate",
                    CreatedDate = DateTime.Now,
                    SuperHeroName = "Superman",
                    SuperHeroPower = "Fly"
                });
                dbContext.SaveChanges();
            }

            return "Superman is generated.";
        }

        public void GenerateThor()
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.SuperHeroes.Add(new SupperHeroModel
                {
                    CreatedBy = "Auto Generate",
                    CreatedDate = DateTime.Now,
                    SuperHeroName = "Thor",
                    SuperHeroPower = "Hammer"
                });
                dbContext.SaveChanges();
            }
        }

        public void GenerateHulk()
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.SuperHeroes.Add(new SupperHeroModel
                {
                    CreatedBy = "Auto Generate",
                    CreatedDate = DateTime.Now,
                    SuperHeroName = "Hulk",
                    SuperHeroPower = "Power"
                });
                dbContext.SaveChanges();
            }
        }

    }
}
