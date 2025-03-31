using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseProject.Entities
{
	public class Role
	{
		//Primary Key
		[Key]
		public int RoleId { get; set; }

		//Additional Attributes
		public string? RoleName { get; set; }
	}
}
