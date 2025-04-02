using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSkinCareBookingRepositories.Base;
using zSkinCareBookingRepositories.DTO;
using zSkinCareBookingRepositories_.Models;

namespace zSkinCareBookingRepositories
{
	public class ScheduleRepository : GenericRepository<Schedule>
	{
		public ScheduleRepository() { }

		public async Task<List<Schedule>> GetAllSchedule()
		{
			return await _context.Schedules.Include(s => s.Therapist).ToListAsync();
		}

		public async Task<int> CreateScedule(ScheduleDTO scheduleDTO)
		{
			Schedule schedule = new Schedule();
			schedule.TherapistId = scheduleDTO.TherapistId;
			schedule.BookingId = scheduleDTO.BookingId;
			schedule.StartFrom = TimeOnly.Parse(scheduleDTO.StartFrom);
			schedule.EndsAt = TimeOnly.Parse(scheduleDTO.EndsAt);
			schedule.Date = DateTime.Parse(scheduleDTO.Date);
			schedule.CreateAtDateTime = DateTime.Now;
			_context.Add(schedule);
			return await _context.SaveChangesAsync();
		}


		public async Task<int> UpdateSceduleById(int scheduleId,ScheduleDTO scheduleDTO)
		{
			 var schedule = await _context.Schedules.FirstOrDefaultAsync(s => s.Id == scheduleId);
			if(schedule == null) 
				return -1;
            schedule.StartFrom = TimeOnly.Parse(scheduleDTO.StartFrom);
            schedule.EndsAt = TimeOnly.Parse(scheduleDTO.EndsAt);
            schedule.Date = DateTime.Parse(scheduleDTO.Date);
            schedule.UpdateAtDateTime = DateTime.Now;
			schedule.BookingId = scheduleDTO.BookingId;
			schedule.TherapistId = scheduleDTO.TherapistId;
			_context.Update(schedule);
			return await _context.SaveChangesAsync();
		}


		public async Task<List<Schedule>> SearchSchedule(DateTime date, TimeOnly startFrom, TimeOnly endsAt)
		{
			return await _context.Schedules.
				Include(s => s.Therapist).
				Where(b => b.Date != date
				||b.StartFrom != startFrom
				|| b.EndsAt != endsAt
				|| b.Date.Equals(date)
				|| b.StartFrom.Equals(startFrom)
				|| b.EndsAt.Equals(endsAt)).ToListAsync();
		}

		public async Task<bool> DeleteSchedulById(int scheduleId)
		{
			var schedule = _context.Schedules.FirstOrDefault(s => s.Id == scheduleId);
			if (schedule == null)
			{
				return false;
			}
			try
			{
				_context.Remove(schedule);
				await _context.SaveChangesAsync();
				return true;

			}catch(Exception ex)
			{
				return false;
			}
		}
	}
}
