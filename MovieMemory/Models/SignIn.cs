using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieMemory.Models
{
    public class SignIn
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please enter proper email address.")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [DisplayName("Remember me")]
        public bool RememberMe { get; set; }
    }
}