using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AadimPark.Models.ViewModels
{
    public class UserVM
    {
       
        [Required]
        [Display(Name = "Email")]

        public string Email { get; set; }
        [Required]
        [Display(Name = "Username")]

        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Display(Name = "Role")]
        public string Role { get; set; }
       
        
        public List<User> Users { get; set; }   
   
    }
}