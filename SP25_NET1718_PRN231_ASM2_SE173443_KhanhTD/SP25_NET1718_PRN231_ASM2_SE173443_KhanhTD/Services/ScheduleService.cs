using Grpc.Core;
using SP25_NET1718_PRN231_ASM2_SE173443_KhanhTD.Protos;
using System.Text.Json.Serialization;
using System.Text.Json;
using zSkinCareBookingServices.InterfaceService;
using zSkinCareBookingServices_.InterfaceService;
using zSkinCareBookingRepositories.DTO;

namespace SP25_NET1718_PRN231_ASM2_SE173443_KhanhTD.Services
{
    public class ScheduleService : ScheduleGRPC.ScheduleGRPCBase
    {

        private readonly ScheduleInterfaceService _scheduleInterfaceService;
        private readonly TherapistServiceInterface _therapistService;

        public ScheduleService(ILogger<ScheduleService> logger, TherapistServiceInterface therapistServiceInterface, ScheduleInterfaceService scheduleInterfaceService)
        {
            _therapistService = therapistServiceInterface;
            _scheduleInterfaceService = scheduleInterfaceService;
        }

        //Done
        public override async Task<ActionResult> GetAllSchedule(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                //Khởi tạo kiểu trả về của proto
                var result = new ScheduleList();

                //Lấy danh sách service từ database lên
                var services = await _scheduleInterfaceService.GetSchedules();

                //Khởi tạo biến để đổi entity thành JSON
                var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                //Biến danh sách service từ database thành JSON
                var servicesString = JsonSerializer.Serialize(services, opt);

                //Biến JSON đó trở về thành kiểu proto
                var items = JsonSerializer.Deserialize<List<Schedule>>(servicesString, opt);

                //Lấy kết quả
                result.Data.AddRange(items);

                //Trả về kết quả
                return await Task.FromResult(new ActionResult() { Status = 200, Message = "Get All Data Successfully", Data = result });

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ActionResult() { Status = -1, Message = "Get All Data Failed" });
            }
        }

        //Done
        public override async Task<Schedule> GetScheduleById(ScheduleIdRequest request, ServerCallContext context)
        {
            try
            {
                var result = new Schedule();

                var service = await _scheduleInterfaceService.GetScheduleById(request.ScheduleId);

                //Khởi tạo biến để đổi entity thành JSON
                var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                //Biến danh sách service từ database thành JSON
                var serviceString = JsonSerializer.Serialize(service, opt);

                //Biến JSON đó trở về thành kiểu proto
                var item = JsonSerializer.Deserialize<Schedule>(serviceString, opt);

                //Lấy kết quả
                result = item;

                //Trả về kết quả
                return await Task.FromResult(result);

            }
            catch (Exception e)
            {
                return await Task.FromResult(new Schedule());
            }
        }

        //Done
        public override async Task<ActionResult> DeleteScheduleById(ScheduleIdRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _scheduleInterfaceService.DeleteSchedule(request.ScheduleId);

                //Trả về kết quả
                return await Task.FromResult(new ActionResult() { Status = 200, Message = "Delete Data Successfully" });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ActionResult() { Status = 200, Message = "Delete Failed" });
            }
        }

        //Done
        public override async Task<ActionResult> AddSchedule(Schedule request, ServerCallContext context)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception();
                }
                else
                {
                    //Khởi tạo biến để đổi entity thành JSON
                    var opt = new JsonSerializerOptions()
                    {
                        ReferenceHandler = ReferenceHandler.IgnoreCycles,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    //Chuyển service thành JSON
                    var service = JsonSerializer.Serialize(request, opt);

                    //Chuyển JSON thành Service Entity
                    var item = JsonSerializer.Deserialize<ScheduleDTO>(service, opt);

                    //Lưu Service xuống database
                    var result = await _scheduleInterfaceService.CreateSchedule(item);

                    if (result == 0)
                    {
                        throw new Exception();
                    }

                    //Trả kết quả
                    return await Task.FromResult(new ActionResult() { Status = 200, Message = "Added Successfully" });
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ActionResult() { Status = -1, Message = "Add Failed" });
            }
        }

        //Done
        public override async Task<ActionResult> EditSchedule(ScheduleUpdate request, ServerCallContext context)
        {
            if (request != null)
            {
                {
                    //Khởi tạo biến để đổi entity thành JSON
                    var opt = new JsonSerializerOptions()
                    {
                        ReferenceHandler = ReferenceHandler.IgnoreCycles,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    //Chuyển service thành JSON

                    var schedule = JsonSerializer.Serialize(request, opt);

                    //Chuyển JSON thành Service Entity
                    var item2 = JsonSerializer.Deserialize<ScheduleDTO>(schedule, opt);

                    //Lưu Service xuống database
                    var result = await _scheduleInterfaceService.UpdateScheduleById(request.Id, item2);
                    return await Task.FromResult(new ActionResult() { Status = 200, Message = "Edit Successfully" });
                }
            }
            return await Task.FromResult(new ActionResult() { Status = -1, Message = "Edit Failed" });

        }

        public override async Task<TherapistList> GetAllTherapist(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                //Khởi tạo kiểu trả về của proto
                var result = new TherapistList();

                //Lấy danh sách service từ database lên
                var services = await _therapistService.GetTherapists();

                //Khởi tạo biến để đổi entity thành JSON
                var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                //Biến danh sách service từ database thành JSON
                var servicesString = JsonSerializer.Serialize(services, opt);

                //Biến JSON đó trở về thành kiểu proto
                var items = JsonSerializer.Deserialize<List<Therapist>>(servicesString, opt);

                //Lấy kết quả
                result.Data.AddRange(items);

                //Trả về kết quả
                return result;

            }
            catch (Exception ex)
            {
                return new TherapistList();
            }

        }
    }
}
