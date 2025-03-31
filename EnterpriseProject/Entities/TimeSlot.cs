using System.ComponentModel.DataAnnotations;

namespace EnterpriseProject.Entities
{
	public class TimeSlot
	{
		[Key]
		public int TimeSlotId { get; set; }

		[Required(ErrorMessage = "Please enter a start time")]
		public DateTime StartTime { get; set; }

		[Required(ErrorMessage = "Please enter an end time")]
		public DateTime EndTime { get; set; }

		// Foreign Key for Schedule (Nullable in case of orphaned dates)
		public int? ScheduleId { get; set; }
		public Schedule Schedule { get; set; }

		// Navigation Property for Appointments
		public ICollection<Appointment> Appointments { get; set; }
	}
}
