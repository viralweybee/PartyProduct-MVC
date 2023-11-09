using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exercise_2.Models
{
    public class Assign
    {
        [Key]
        public int id { get; set; }
       
        public Party Party { get; set; }
        [Required]
        [ForeignKey("Party")]
        public int PartyId { get; set; }

        public Product Product { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

    }
}