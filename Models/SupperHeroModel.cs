﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace DotnetFramework48Lab01.Models
{
    public class SupperHeroModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SuperHeroName { get; set; }
        public string SuperHeroPower { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte[] Image { get; set; }
    }

    public class SupperHeroVM
    {
        public int Id { get; set; }
        [Required]
        public string SuperHeroName { get; set; }
        public string SuperHeroPower { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}