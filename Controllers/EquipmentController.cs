using DotnetFramework48Lab01.Filters;
using DotnetFramework48Lab01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotnetFramework48Lab01.Controllers
{
    [LogActionFilter]
    public class EquipmentController : Controller
    {
        private static List<EquipmentCheck> equipmentList = new List<EquipmentCheck>
        {
            new EquipmentCheck
            {
                Id = 1,
                EquipmentName = "เครื่องจักร A",
                DailyStatuses = new Dictionary<int, string>
                {
                    { 1, "Checked" }, { 2, "Checked" }, { 3, "Partially Checked" },
                    { 4, "Not Checked" }, { 5, "Equipment Breakdown" }, { 6, "Under Maintenance" }
                },
                LeaderRechecks = new List<LeaderRecheck>
                {
                    new LeaderRecheck { LeaderName = "นายสมชาย", RecheckDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 5) },
                    new LeaderRecheck { LeaderName = "คุณวิไล", RecheckDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 20) }
                },
                ManagementName = "คุณมานพ",
                ManagementApproveDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 25)
            },
            new EquipmentCheck
            {
                Id = 2,
                EquipmentName = "เครื่องจักร B",
                DailyStatuses = new Dictionary<int, string>
                {
                    { 9, "Checked" }, { 7, "Checked" }, { 3, "Partially Checked" },
                    { 2, "Not Checked" }, { 5, "Equipment Breakdown" }, { 6, "Under Maintenance" }
                },
                LeaderRechecks = new List<LeaderRecheck>
                {
                    new LeaderRecheck { LeaderName = "นายสมชาย", RecheckDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 5) },
                    new LeaderRecheck { LeaderName = "คุณวิไล", RecheckDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 20) }
                },
                ManagementName = "คุณมานพ",
                ManagementApproveDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 25)
            },
            new EquipmentCheck
            {
                Id = 2,
                EquipmentName = "เครื่องจักร C",
                DailyStatuses = new Dictionary<int, string>
                {
                    { 9, "Checked" }, { 23, "Checked" }, { 13, "Partially Checked" },
                    { 2, "Not Checked" }, { 4, "Equipment Breakdown" }, { 14, "Under Maintenance" }
                },
                LeaderRechecks = new List<LeaderRecheck>
                {
                    new LeaderRecheck { LeaderName = "นายสมชาย", RecheckDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 5) },
                    new LeaderRecheck { LeaderName = "คุณวิไล", RecheckDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 20) }
                },
                ManagementName = "คุณมานพ",
                ManagementApproveDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 25)
            }
        };

        public ActionResult Index()
        {
            return View(equipmentList);
        }

        public ActionResult DayDetails(int equipmentId, int day)
        {
            var equipment = equipmentList.FirstOrDefault(e => e.Id == equipmentId);
            if (equipment == null)
            {
                return HttpNotFound();
            }

            var status = equipment.DailyStatuses.ContainsKey(day) ? equipment.DailyStatuses[day] : "No Data";

            ViewBag.EquipmentName = equipment.EquipmentName;
            ViewBag.Day = day;
            ViewBag.Status = status;
            return View();
        }

        public ActionResult EquipmentChecklist(int equipmentId)
        {
            var equipment = equipmentList.FirstOrDefault(e => e.Id == equipmentId);
            if (equipment == null)
            {
                return HttpNotFound();
            }

            ViewBag.EquipmentName = equipment.EquipmentName;
            ViewBag.ChecklistItems = new List<string>
        {
            "ตรวจสอบระดับน้ำมัน",
            "ตรวจสอบความดันลม",
            "ตรวจสอบอุณหภูมิ",
            "ตรวจสอบระบบไฟฟ้า"
        };

            return View();
        }

        public ActionResult Checklist()
        {
            var equipmentList = new List<Equipment>
            {
                new Equipment { Id = 1, Name = "Scanner" },
                new Equipment { Id = 2, Name = "Battery" },
                new Equipment { Id = 3, Name = "Screen Display" }
            };

            return View(equipmentList);
        }

        [HttpPost]
        public ActionResult SubmitChecklist(List<EquipmentChecklist> checklist)
        {
            if (checklist != null)
            {
                // บันทึกข้อมูลลงฐานข้อมูล (ยังไม่รวมการเชื่อมต่อฐานข้อมูล)
            }

            return RedirectToAction("Checklist");
        }

    }

    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EquipmentChecklist
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string Status { get; set; }  // "OK" หรือ "Not OK"
        public string Remark { get; set; }  // คำอธิบายเพิ่มเติม
    }
}