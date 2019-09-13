using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrangeAPI.Models
{
    public class UserAdmin
    {
        [Key]
        public int IdAdmin { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string TypeUser { get; set; }

        [ForeignKey(nameof(TypeUser))]
        public virtual TypeOfUser TypeOfUser { get; set; }
    }
}