using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseProject.Entities
{
	public class Schedule
	{
		//Primary Key
		[Key]
		public int ScheduleId { get; set; }

		[ForeignKey("PractitionerId")]
		public int PractitionerId { get; set; }
		public Practitioner Practitioner { get; set; }

		// AvailableTimes and BookkedTimes store references for the TimeSlots
		public List<int> AvailableTimes { get; set; } = new List<int>();
		public List<int> BookedTimes { get; set; } = new List<int>();

		// Navigation Property for OutDates (e.g., Time slots)
		public ICollection<TimeSlot> TimeSlot { get; set; }
	}
}
