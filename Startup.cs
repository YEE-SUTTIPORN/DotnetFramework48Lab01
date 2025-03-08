using DotnetFramework48Lab01.Filters;
using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DotnetFramework48Lab01.Startup))] // แก้ MyNamespace ให้ตรงกับโปรเจกต์ของคุณ

namespace DotnetFramework48Lab01
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // ตั้งค่าให้ Hangfire ใช้ SQL Server
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");

            // เปิดหน้า Dashboard `/hangfire`
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });
            app.UseHangfireServer();
        }
    }
}
