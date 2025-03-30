using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSkinCareBookingRepositories;
using zSkinCareBookingRepositories.DTO;
using zSkinCareBookingRepositories_.Models;
using zSkinCareBookingServices.InterfaceService;

namespace zSkinCareBookingServices.ImplementService
{
	public class ScheduleImplementService : ScheduleInterfaceService
	{
		private readonly ScheduleRepository _scheduleRepository;

		public ScheduleImplementService(ScheduleRepository scheduleRepository)
		{
			_scheduleRepository = scheduleRepository;
		}

		public async Task<int> CreateSchedule(ScheduleDTO scheduleDTO)
		{
			return await _scheduleRepository.CreateScedule(scheduleDTO);
		}

		public Task<bool> DeleteSchedule(int scheduleId)
		{
			throw new NotImplementedException();
		}

		//public async Task<int> CreateSchedule(ScheduleDTO scheduleDTO)
		//{
		//	return await _scheduleRepository.CreateScedule(scheduleDTO);
		//}

		//public Task<bool> DeleteSchedule(int scheduleId)
		//{
		//	throw new NotImplementedException();
		//}

		public async Task<bool> DeleteScheduleByIdV2(int scheduleId)
		{
			return await _scheduleRepository.DeleteSchedulById(scheduleId);
			//return false;
		}

		public async Task<Schedule> GetScheduleById(int scheduleId)
		{
			return await _scheduleRepository.GetByIdAsync(scheduleId);
		}

		public async Task<List<Schedule>> GetSchedules()
		{

			return await _scheduleRepository.GetAllSchedule();
		}

		public async Task<List<Schedule>> Search(DateTime date, TimeOnly startTime, TimeOnly endTime)
		{
			return await _scheduleRepository.SearchSchedule(date, startTime, endTime);
		}

		public async Task<int> UpdateSchedule(Schedule schedule)
		{
			return await _scheduleRepository.UpdateAsync(schedule);
		}

		public async Task<int> UpdateScheduleById(int scheduleId, ScheduleDTO scheduleDTO)
		{
			return await _scheduleRepository.UpdateSceduleById(scheduleId, scheduleDTO);
		}
	}
}
