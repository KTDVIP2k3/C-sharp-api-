using HotChocolate.Fetching;
using System.Net;
using zSkinCareBookingRepositories_.Models;
using zSkinCareBookingRepositories_.Response;
using zSkinCareBookingServices.InterfaceService;
using zSkinCareBookingServices_.InterfaceService;

namespace SP25_NET1718_ASM4_SE173443_KhanhTD.GrapQls
{
    public class Query
    {
        private readonly TherapistServiceInterface _therapistServiceInterface;
        private readonly ScheduleInterfaceService _scheduleServiceInterface;

        public Query(TherapistServiceInterface therapistServiceInterface, ScheduleInterfaceService scheduleServiceInterface)
        {
            _therapistServiceInterface = therapistServiceInterface;
            _scheduleServiceInterface = scheduleServiceInterface;
        }

        [GraphQLName("getAllSchedule")]
        [GraphQLDescription("Get all schedule")]
        [SkipDataLoaderCache]

        public async Task<List<Schedule>> GetAllSchedule()
        {
            try
            {
                var result = await _scheduleServiceInterface.GetSchedules();
                return result;
            }
            catch (Exception ex)
            {
                return new List<Schedule> { new Schedule() };
            }
        }
        [GraphQLName("getAllScheduleV2")]
        [GraphQLDescription("Get all schedule v2")]
        [SkipDataLoaderCache]
        public async Task<ResponseObj> GetAllScheduleV2()
        {
            try
            {
                var schedules = await _scheduleServiceInterface.GetSchedules();  

              
                return new ResponseObj
                {
                    Message = "Danh sách schedule",
                    Status = HttpStatusCode.OK.ToString(),
                    Data = schedules 
                };
            }
            catch (Exception ex)
            {
           
                return new ResponseObj
                {
                    Message = ex.Message,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Data = new List<Schedule>() 
                };
            }
        }

        [GraphQLName("getScheduleById")]
        [GraphQLDescription("Get a schedule by its ID")]
        public async Task<Schedule> GetSchedule(int id)
        {
            try
            {
                var result = await _scheduleServiceInterface.GetScheduleById(id);
                return result;
            }
            catch (Exception ex)
            {
                return new Schedule();
            }
        }

        [GraphQLName("getAllTherapist")]
        [GraphQLDescription("Get all therapist")]
        public async Task<List<Therapist>> GetAllTherapist()
        {
            try
            {
                return await _therapistServiceInterface.GetTherapists();
            }
            catch (Exception e)
            {
                return new List<Therapist> { new Therapist() };
            }
        }
    }
}
