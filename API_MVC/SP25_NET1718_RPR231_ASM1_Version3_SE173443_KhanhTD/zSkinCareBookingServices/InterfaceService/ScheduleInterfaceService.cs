using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSkinCareBookingRepositories.DTO;
using zSkinCareBookingRepositories_.Models;

namespace zSkinCareBookingServices.InterfaceService
{
	public interface ScheduleInterfaceService
	{
		Task<int> CreateSchedule(ScheduleDTO scheduleDTO);

		Task<bool> DeleteSchedule(int scheduleId);

		Task<List<Schedule>> GetSchedules();

		Task<int> UpdateScheduleById(int scheduleId, ScheduleDTO scheduleDTO);

		Task<Schedule> GetScheduleById(int scheduleId);

		Task<List<Schedule>> Search(DateTime date, TimeOnly startTime, TimeOnly endTime);

		Task<int> UpdateSchedule(Schedule schedule);

		Task<bool> DeleteScheduleByIdV2(int scheduleId);
	}
}
