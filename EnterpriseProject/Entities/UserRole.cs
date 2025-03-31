namespace EnterpriseProject.Entities
{
	public class UserRole
	{
		//Composite Key
		public int RoleId { get; set; }
		public int UserId { get; set; }

		//Navigation Properties
		public Role? Role { get; set; }
		public User? User { get; set; }	
	}
}
