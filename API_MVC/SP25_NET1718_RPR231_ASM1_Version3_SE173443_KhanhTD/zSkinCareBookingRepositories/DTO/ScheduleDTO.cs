using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zSkinCareBookingRepositories.DTO
{
	public class ScheduleDTO
	{
		public int TherapistId { get; set; }
		public int BookingId { get; set; }
		public DateTime Date { get; set; }
		public TimeOnly StartFrom { get; set; }
		public TimeOnly EndsAt { get; set; }
	}

}
