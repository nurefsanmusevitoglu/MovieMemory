using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieMemory.Models
{
    public class SignUp
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter proper email address.")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
         
        [Required]
        [DisplayName("Reneter Password")]
        [Compare("Password", ErrorMessage ="Passwords do not match.")]
        public string RePassword { get; set; }
    }
}