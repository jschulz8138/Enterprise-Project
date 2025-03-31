using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EnterpriseProject.Entities
{
	public class User : IdentityUser
    {
		[Key]
		public int UserId { get; set; }

		[Required(ErrorMessage = "Please enter your first name.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please enter your last name.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Please enter your email address.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please enter your password.")]
		public string Password { get; set; }
	}
}
