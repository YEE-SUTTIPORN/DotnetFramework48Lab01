using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public UserModel User { get; set; }
        [NotMapped]
        public RoleModel Role { get; set; }
    }
}