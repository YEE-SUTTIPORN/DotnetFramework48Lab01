using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotnetFramework48Lab01.Controllers
{
    public class ChecklistController : Controller
    {
        // Static variable สำหรับจำลองข้อมูล Checklist
        private static List<ChecklistItem> checklist;

        // Static Constructor โหลดข้อมูลเพียงครั้งเดียว
        static ChecklistController()
        {
            checklist = new List<ChecklistItem>
            {
                new ChecklistItem { Id = 1, Equipment = "โฟรคลิฟ-001", Question = "ไฟหน้าใช้ได้ไหม", InputType = "boolean" },
                new ChecklistItem { Id = 2, Equipment = "โฟรคลิฟ-001", Question = "ไฟท้ายใช้ได้ไหม", InputType = "boolean" },
                new ChecklistItem { Id = 3, Equipment = "โฟรคลิฟ-001", Question = "ล้อหน้าทั้งสองข้างแบนไหม", InputType = "boolean" },
                new ChecklistItem { Id = 4, Equipment = "โฟรคลิฟ-001", Question = "ล้อหลังทั้งสองข้างแบนไหม", InputType = "boolean" },
                new ChecklistItem { Id = 5, Equipment = "โฟรคลิฟ-001", Question = "เลขไมค์เท่าไหร่", InputType = "text" },
                new ChecklistItem { Id = 6, Equipment = "โฟรคลิฟ-001", Question = "น้ำมันล่าสุดเท่าไหร่", InputType = "text" }
            };
        }

        // GET: /Checklist/Index/{id}
        public ActionResult Index(int id = 1)
        {
            var item = checklist.FirstOrDefault(c => c.Id == id);
            if (item == null)
            {
                return RedirectToAction("Complete");
            }
            return View(item);
        }

        // POST: /Checklist/Index
        [HttpPost]
        public ActionResult Index(ChecklistItem model, string action)
        {
            // อัปเดตคำตอบใน static list
            var item = checklist.FirstOrDefault(c => c.Id == model.Id);
            if (item != null)
            {
                item.Answer = model.Answer;
                item.Comment = model.Comment;
                item.DamageReason = model.DamageReason;
            }

            // ตรวจสอบว่ากดปุ่ม Back หรือ Next
            if (action == "back")
            {
                int prevId = model.Id - 1;
                if (checklist.Any(c => c.Id == prevId))
                    return RedirectToAction("Index", new { id = prevId });
                else
                    return RedirectToAction("Index", new { id = model.Id }); // หากเป็นรายการแรก
            }
            else if (action == "next")
            {
                int nextId = model.Id + 1;
                if (checklist.Any(c => c.Id == nextId))
                    return RedirectToAction("Index", new { id = nextId });
                else
                    return RedirectToAction("Complete"); // หากครบทุกข้อแล้ว
            }
            return View(model);
        }

        // หน้า Complete แสดงสรุปผลการตรวจสอบทั้งหมด
        public ActionResult Complete()
        {
            return View(checklist);
        }
    }

    public class ChecklistItem
    {
        public int Id { get; set; }
        public string Equipment { get; set; }
        public string Question { get; set; }
        // กำหนดชนิดของ Input: "boolean" สำหรับ Radio Button หรือ "text" สำหรับ Text Input
        public string InputType { get; set; }
        // สำหรับเก็บคำตอบจาก Radio Button (เช่น "OK" หรือ "Not OK")
        public string Answer { get; set; }
        // สำหรับเก็บข้อความเพิ่มเติมในกรณีที่เป็น Input Text
        public string Comment { get; set; }
        // สำหรับเก็บสาเหตุความเสียหาย (เมื่อ Answer เป็น Not OK)
        public string DamageReason { get; set; }

    }
}