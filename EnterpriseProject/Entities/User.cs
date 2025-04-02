using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EnterpriseProject.Entities
{
    public class User : IdentityUser<int>
    {

        [Required(ErrorMessage = "Please enter your first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }

        // Email is already part of IdentityUser, so no need to redefine it.
        // public string Email { get; set; }  <-- This is redundant.

        // Password is not needed, as IdentityUser uses PasswordHash
        // public string Password { get; set; }  <-- This is redundant.
    }
}
