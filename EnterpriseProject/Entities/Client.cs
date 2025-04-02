using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseProject.Entities
{
    public class Client
    {
		//Primary Key
		[Key]
		public int ClientId { get; set; }

		//Foreign Key
		[ForeignKey("UserId")]
		public int UserId { get; set; }

        //Additional Attributes
        public User User { get; set; }  // Navigation property
    }
}
