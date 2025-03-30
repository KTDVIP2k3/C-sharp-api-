using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zSkinCareBookingRepositories_.DTO
{
	public class TherapistDTO
	{
		public int Id { get; set; }
		public int UserId { get; set; }

		public string Fullname { get; set; } = null!;

		public string? Phone { get; set; }

		public string? Email { get; set; }

		public string? Specialization { get; set; }

		public int? ExpMonth { get; set; }

		public string? Bio { get; set; }

	}
}
