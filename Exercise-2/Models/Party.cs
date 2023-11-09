using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercise_2.Models
{
    public class Party
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string p_name { get; set; }
    }
}