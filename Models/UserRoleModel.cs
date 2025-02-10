using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotnetFramework48Lab01.Models
{
    public class UserRoleModel
    {
        [Key]
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public UserModel User { get; set; }
        public RoleModel Role { get; set; }
    }
}