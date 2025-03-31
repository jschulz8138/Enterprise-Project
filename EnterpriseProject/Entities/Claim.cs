using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseProject.Entities
{
	public class Claim
	{
		//Primary Key
		[Key]
		public int ClaimId { get; set; }

		//Foreign Key
		[ForeignKey("AppointmentId")]
        public int AppointmentId { get; set; }
	}
}
