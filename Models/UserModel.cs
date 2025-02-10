using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotnetFramework48Lab01.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }
        [StringLength(100)]
        public string Username { get; set; }
        [StringLength(255)]
        public string PasswordHash { get; set; }
    }
}