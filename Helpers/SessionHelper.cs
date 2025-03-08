using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotnetFramework48Lab01.Helpers
{
    public static class SessionKey
    {
        public const string SectionID = "SectionID";
        public const string UserID = "UserID";
    }

    public class SessionHelper
    {
        /// <summary>
        /// ดึงค่า Session ตาม key ที่กำหนด
        /// </summary>
        public static string GetSessionValue(string key)
        {
            var session = HttpContext.Current?.Session;
            if (session != null && session[key] != null)
            {
                return session[key].ToString();
            }
            return null; // หรือค่า default ที่ต้องการ
        }

        /// <summary>
        /// ตั้งค่าค่าให้กับ Session
        /// </summary>
        public static void SetSessionValue(string key, string value)
        {
            var session = HttpContext.Current?.Session;
            if (session != null)
            {
                session[key] = value;
            }
        }

        /// <summary>
        /// ลบค่า Session ตาม key
        /// </summary>
        public static void RemoveSessionValue(string key)
        {
            var session = HttpContext.Current?.Session;
            if (session != null)
            {
                session.Remove(key);
            }
        }
    }
}