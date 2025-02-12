using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotnetFramework48Lab01.Models
{
    public class EquipmentCheck
    {
        public int Id { get; set; }
        public string EquipmentName { get; set; }
        public Dictionary<int, string> DailyStatuses { get; set; } = new Dictionary<int, string>();
        public List<LeaderRecheck> LeaderRechecks { get; set; } = new List<LeaderRecheck>();
        public string ManagementName { get; set; }
        public DateTime? ManagementApproveDate { get; set; }
    }
}