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
        public int IdCategory { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public byte Image { get; set; }

        public bool State { get; set; }
    }
}