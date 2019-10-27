using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrangeAPI.Models
{
    public class EndUser
    {
        [Key]
        public int IdEndUser { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        public bool State { get; set; }

        [ForeignKey(nameof(IdUserType))]
        public TypeOfUser typeOfUser { get; set; }
        public int IdUserType { get; set; }
    }
}