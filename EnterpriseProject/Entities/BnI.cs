using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseProject.Entities
{
	public class BnI
	{
		//Primary Key
		[Key]
		public int BnIId { get; set; }

		[ForeignKey("UserId")]
		public int UserId { get; set; }

		//Additional Attributes
	}
}
