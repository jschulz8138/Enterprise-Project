using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseProject.Entities
{
	public class Claim
	{
		//Primary Key
		[Key]
		private int ClaimId { get; set; }

		//Foreign Key
		[ForeignKey("AppointmentId")]
		private int AppointmentId { get; set; }
	}
}
