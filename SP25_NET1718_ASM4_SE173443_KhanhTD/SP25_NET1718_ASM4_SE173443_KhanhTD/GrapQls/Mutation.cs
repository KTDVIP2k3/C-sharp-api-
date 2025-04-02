using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net;
using zSkinCareBookingRepositories.DTO;
using zSkinCareBookingRepositories_.Models;
using zSkinCareBookingServices.InterfaceService;
using zSkinCareBookingServices_.InterfaceService;

namespace SP25_NET1718_ASM4_SE173443_KhanhTD.GrapQls
{
    public class Mutation
    {
        private readonly ScheduleInterfaceService _scheduleInterfaceService;

        public Mutation(ScheduleInterfaceService scheduleInterfaceService)
        {
            _scheduleInterfaceService = scheduleInterfaceService;
        }

        [GraphQLName("createSchedule")]
        [GraphQLDescription("Create new schedule")]
        public async Task<int> CreateSchedule(ScheduleDTO scheduleDTO)
        {
            return await _scheduleInterfaceService.CreateSchedule(scheduleDTO);
        }

        [GraphQLName("updateSchedule")]
        [GraphQLDescription("Update exist schedule by Id")]
        public async Task<int> UpdateSchedule(int scheduleId, ScheduleDTO scheduleDTO)
        {
            return await _scheduleInterfaceService.UpdateScheduleById(scheduleId, scheduleDTO);
        }

        [GraphQLName("deleteSchedule")]
        [GraphQLDescription("Delete schedule by Id")]
        public async Task<bool> DeleteSchedule(int scheduleId)
        {
            return await _scheduleInterfaceService.DeleteScheduleByIdV2(scheduleId);
        }

    }
}
