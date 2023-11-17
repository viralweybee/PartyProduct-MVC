using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exercise_2.Models
{
    public class Invoice
    {
        [Key]
        public int id { get; set; }
        public Party Party { get; set; }
        public Product Product { get; set; }
        public int rate  { get; set; }
        public int quantity { get; set; }
    }
}