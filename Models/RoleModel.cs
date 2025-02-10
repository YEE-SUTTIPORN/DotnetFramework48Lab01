using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotnetFramework48Lab01.Models
{
    public class RoleModel
    {
        [Key]
        public int RoleID { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; }
    }
}