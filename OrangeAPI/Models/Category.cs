using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrangeAPI.Models
{
    public class Category
    {
        [Key]
        public Int32 Id { get; set; }

        [Required]
        [StringLength(30)]
        public String Name { get; set; }

        public Byte Image { get; set; }
    }
}