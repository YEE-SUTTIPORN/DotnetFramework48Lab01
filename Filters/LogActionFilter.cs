using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotnetFramework48Lab01.Filters
{
    public class LogActionFilter : ActionFilterAttribute, IExceptionFilter
    {
        private Stopwatch stopwatch;
        private string _username = string.Empty;

        public LogActionFilter()
        {
            _username = "Anonymous";
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            stopwatch = Stopwatch.StartNew(); // ⏳ เริ่มจับเวลาตั้งแต่เรียก Controller

            string log = $"[START] - Controller: {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName}, Action: {filterContext.ActionDescriptor.ActionName}";
            WriteLog(log);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            stopwatch.Stop(); // ⏳ หยุดจับเวลา
            double executionTime = stopwatch.Elapsed.TotalMilliseconds;

            string log = $"[ END ] - Execution Time: {executionTime} ms";
            WriteLog(log);
        }

        public void OnException(ExceptionContext filterContext)
        {
            string log = $"[ERROR] - Controller: {filterContext.RouteData.Values["controller"]}, Action: {filterContext.RouteData.Values["action"]}, Error: {filterContext.Exception.Message}";
            WriteLog(log);

            // ✅ จัดการให้ระบบไปหน้าข้อผิดพลาด (Optional)
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectResult("~/Error/InternalServerError");
        }

        private void WriteLog(string message)
        {
            string logDirectory = HttpContext.Current != null
                ? HttpContext.Current.Server.MapPath("~/App_Data/Logs/")
                : Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data", "Logs");

            // ✅ กำหนดชื่อไฟล์ Log ตามวัน
            string logFileName = $"action_log_{DateTime.Now:yyyy-MM-dd}.txt";
            string logFilePath = Path.Combine(logDirectory, logFileName);

            // ✅ ตรวจสอบว่ามีโฟลเดอร์ Logs หรือไม่ ถ้าไม่มีให้สร้าง
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // ✅ เขียน Log ลงไฟล์
            File.AppendAllText(logFilePath, $"[{DateTime.Now:HH:mm:ss}] {message}, By: {_username}{Environment.NewLine}");
        }

    }
}