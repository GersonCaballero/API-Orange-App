using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrangeAPI.Models
{
    public class TypeOfUser
    {
        [Key]
        public int IdUserType { get; set; }

        [Required]
        public string Type { get; set; }
    }
}