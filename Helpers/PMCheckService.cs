using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotnetFramework48Lab01.Helpers
{
    public class PMCheckService
    {
        // 🟢 ตรวจสอบว่า "ตอนนี้" ควรสร้าง PM Check ประเภทใด
        public string GetPMCheckType(DateTime currentTime)
        {
            if (IsDailyShiftDay(currentTime))
            {
                return "Daily Shift Day";
            }
            else if (IsDailyShiftNight(currentTime))
            {
                return "Daily Shift Night";
            }
            else if (IsFirstWorkingDayOfWeek(currentTime))
            {
                return "Weekly";
            }
            else if (IsFirstWorkingDayOfMonth(currentTime))
            {
                return "Monthly";
            }

            return "None"; // ไม่มี PM Check ในวันนี้
        }

        // 🔹 1. ตรวจสอบว่าอยู่ในช่วง Daily Shift Day หรือไม่ (08:30 - 20:29)
        private bool IsDailyShiftDay(DateTime dateTime)
        {
            TimeSpan time = dateTime.TimeOfDay;
            return time >= TimeSpan.FromHours(8).Add(TimeSpan.FromMinutes(30)) && time < TimeSpan.FromHours(20).Add(TimeSpan.FromMinutes(30));
        }

        // 🔹 2. ตรวจสอบว่าอยู่ในช่วง Daily Shift Night หรือไม่ (20:30 - 08:29)
        private bool IsDailyShiftNight(DateTime dateTime)
        {
            TimeSpan time = dateTime.TimeOfDay;
            return time >= TimeSpan.FromHours(20).Add(TimeSpan.FromMinutes(30)) || time < TimeSpan.FromHours(8).Add(TimeSpan.FromMinutes(30));
        }

        // 🔹 3. ตรวจสอบว่าวันนี้เป็นวันแรกของสัปดาห์ที่ทำงานหรือไม่
        private bool IsFirstWorkingDayOfWeek(DateTime date)
        {
            List<DayOfWeek> weekend = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };

            // หาวันจันทร์แรกของสัปดาห์ปัจจุบัน
            DateTime firstDayOfWeek = date.Date;
            while (firstDayOfWeek.DayOfWeek != DayOfWeek.Monday)
            {
                firstDayOfWeek = firstDayOfWeek.AddDays(-1);
            }

            // ถ้าวันจันทร์เป็นวันหยุด → เลื่อนไปวันถัดไปจนกว่าจะเจอวันทำงาน
            while (weekend.Contains(firstDayOfWeek.DayOfWeek) || IsHoliday(firstDayOfWeek))
            {
                firstDayOfWeek = firstDayOfWeek.AddDays(1);
            }

            return date.Date == firstDayOfWeek;
        }

        // 🔹 4. ตรวจสอบว่าวันนี้เป็นวันแรกของเดือนที่ทำงานหรือไม่
        private bool IsFirstWorkingDayOfMonth(DateTime date)
        {
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

            // ถ้าวันที่ 1 เป็นวันหยุด → เลื่อนไปหาวันทำงานวันแรกของเดือน
            while (IsHoliday(firstDayOfMonth) || firstDayOfMonth.DayOfWeek == DayOfWeek.Saturday || firstDayOfMonth.DayOfWeek == DayOfWeek.Sunday)
            {
                firstDayOfMonth = firstDayOfMonth.AddDays(1);
            }

            return date.Date == firstDayOfMonth;
        }

        // 🔹 5. ฟังก์ชันเช็ควันหยุด (สามารถดึงจากฐานข้อมูล)
        private bool IsHoliday(DateTime date)
        {
            List<DateTime> holidays = new List<DateTime>
        {
            new DateTime(date.Year, 1, 1),  // วันปีใหม่
            new DateTime(date.Year, 4, 13), // วันสงกรานต์ (ตัวอย่าง)
        };

            return holidays.Contains(date.Date);
        }
    }

}