using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrangeAPI.Models
{
    public class Biker
    {
        [Key]
        public int IdBiker { get; set; }

        [Required]
        public string Name{ get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Age { get; set; }
        public bool State { get; set; }

        [ForeignKey(nameof(IdUserType))]
        public TypeOfUser typeOfUser { get; set; }
        public int IdUserType { get; set; }
    }
}