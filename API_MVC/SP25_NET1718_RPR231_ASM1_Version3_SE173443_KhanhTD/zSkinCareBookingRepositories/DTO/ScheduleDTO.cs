﻿using System;
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
		public String Date { get; set; }
		public String StartFrom { get; set; }
		public String EndsAt { get; set; }
	}

}
