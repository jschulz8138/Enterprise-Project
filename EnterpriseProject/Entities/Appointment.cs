using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseProject.Entities
{
	public class Appointment
	{
		//Composite Key
		public int PractitionerId { get; set; }
		public int ClientId { get; set; }
		public int DateId { get; set; }

		//Additional Attributes
		public bool IsPaid { get; set; }

		//Navigation Properties
		[ForeignKey("PractitionerId")]
		public Practitioner Practitioner { get; set; }
		[ForeignKey("ClientId")]
		public Client Client { get; set; }
		[ForeignKey("DateId")]
		public TimeSlot TimeSlot { get; set; }
		public Claim? Claim { get; set; }		//THis needs to be nullable because it is a 1 to 0..1 relationship.

	}
}
