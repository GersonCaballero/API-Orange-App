using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrangeAPI.Models
{
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }

        public int IdCommerce { get; set; }

        [ForeignKey("IdCommerce")]
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Image { get; set; }
        public double Precio { get; set; }
        public int Quantity { get; set; }
    }
}