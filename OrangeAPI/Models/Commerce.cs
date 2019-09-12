using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrangeAPI.Models
{
    public class Commerce
    {
        [Key]
        public int IdCommerce { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string RTN { get;  set; }

        public byte Image { get; set; }

        public int IdCategory { get; set; }

        [ForeignKey(nameof(IdCategory))]
        public virtual Category Category { get; set; }
    }
}