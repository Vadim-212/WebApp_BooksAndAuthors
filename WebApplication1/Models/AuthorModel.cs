using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25,MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30,MinimumLength = 2)]
        public string LastName { get; set; }
    }
}