
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSkinCareBookingRepositories.Base;
using zSkinCareBookingRepositories_.Models;

namespace zSkinCareBookingRepositories
{
	public class TherapistRepository : GenericRepository<Therapist>
	{
		public TherapistRepository() { }

		public async Task<List<Therapist>> GetAllTherapist()
		{
			return await _context.Therapists.Include(b => b.Schedules).ToListAsync();
		}

		public async Task<List<Therapist>> SearchTherapist(String fullName, String phone, String email, String specialization, int exp, String bio)
		{
			return await _context.Therapists.
				Include(t => t.Schedules)
				.Where(s => string.IsNullOrEmpty(fullName)
				|| string.IsNullOrEmpty(phone) 
				|| string.IsNullOrEmpty(email)
				|| string.IsNullOrEmpty(bio) 
				|| s.Fullname.Contains(fullName)
				|| s.Phone.Contains(phone)
				|| s.Email.Contains(email)).ToListAsync();
		}

		public async Task<bool> DeleteTherapistById(int threrapistId)
		{
			var therapist = await _context.Therapists.FirstOrDefaultAsync(t => t.Id == threrapistId);
			if(therapist != null)
			{
				_context.Remove(therapist);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}
	}
}
