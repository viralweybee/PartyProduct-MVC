using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercise_2.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string pr_name { get; set; }
    }
}