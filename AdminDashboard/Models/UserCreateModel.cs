﻿using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Models
{
    public class UserCreateModel
    {

        [Required, MaxLength(200), MinLength(3), Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, MaxLength(200), MinLength(3), Display(Name = "Last Name")]
        public string LastName { get; set; }
    
        [Required, EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required, MinLength(6), MaxLength(16)]
        [Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, Compare("Password")]
        [Display(Name = "Confirm Password"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
       // [Required]
       // public string Role { get; set; }
    }
}

