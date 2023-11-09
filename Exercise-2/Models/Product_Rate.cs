using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exercise_2.Models
{
    public class Product_Rate
    {
        [Key]
       public int id { get; set; }
        public Product Product { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int Pr_id { get; set; }
        public int rate { get; set; }
        public DateTime date { get; set; }
    }
}