using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using zSkinCareBookingRepositories.DTO;
using zSkinCareBookingRepositories_.Models;
using zSkinCareBookingServices.InterfaceService;

namespace zSkinCareBookin.ApiService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScheduleController : ControllerBase
	{
		private readonly ScheduleInterfaceService _scheduleInterfaceService;
		public ScheduleController(ScheduleInterfaceService scheduleInterfaceService)
		{
			_scheduleInterfaceService = scheduleInterfaceService;
		}

		[HttpGet("GetAllSchedule")]
		[EnableQuery]
		[Authorize]
		public async Task<IActionResult> GetAllSchedule()
		{
			List<Schedule> schedules = await _scheduleInterfaceService.GetSchedules();
			if (schedules.IsNullOrEmpty())
			{
				return NotFound(new {message = "Danh sách schedule đang trống", data = schedules, status = HttpStatusCode.NotFound});
			}
			return Ok(new {message = "Danh sách schedule", data = schedules, staus = HttpStatusCode.OK});
		}

		[HttpGet("GetScheduleById/{scheduleId}")]
		[Authorize]
		public async Task<IActionResult> GetScheduleById(int scheduleId)
		{
			try
			{
				Schedule schedule = await _scheduleInterfaceService.GetScheduleById(scheduleId);
				if (schedule == null)
				{
					return NotFound(new { message = "Id này không tồn tại", data = schedule, status = HttpStatusCode.NotFound });
				}
				return Ok(new { message = "Schedule", data = schedule, status = HttpStatusCode.OK });
			}catch (Exception ex)
			{
				return BadRequest(ex);
			}
   //         Schedule schedule1 = await _scheduleInterfaceService.GetScheduleById(scheduleId);
			//return schedule1;
		}

		[HttpPost("CreateSchedule")]
		[Authorize]
		public async Task<IActionResult> CreateSchedule([FromBody] ScheduleDTO scheduleDTO)
		{
			if(await _scheduleInterfaceService.CreateSchedule(scheduleDTO) > 0)
			{
				return Ok(new { message = "Tạo schedule thành công!" });
			}
			return BadRequest(new {mesage = "Tạo schedule thất bại!"});
		}

		[HttpPut("UpdateScheduleById/{scheduleId}")]
		[Authorize]
		public async Task<IActionResult> UpdateScheduleById(int scheduleId, [FromBody] ScheduleDTO scheduleDTO)
		{
			try
			{
				if (await _scheduleInterfaceService.UpdateScheduleById(scheduleId, scheduleDTO) == 0)
					return BadRequest(new { mesage = "Cập Nhật schedule thất bại!" });

				if(await _scheduleInterfaceService.UpdateScheduleById(scheduleId, scheduleDTO) == -1)
				{
					return NotFound(new { mesage = "Không tìm thấy scheudl có id này để cập nhật" });
				}
				return Ok(new { message = "Cập nhật schedule thành công!" });
			}catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("DeleteScheduleById/{scheduleId}")]
		[Authorize]
		public async Task<IActionResult> deleteScheduleById(int scheduleId)
		{
			try
			{
				if(await _scheduleInterfaceService.DeleteScheduleByIdV2(scheduleId))
				{
					return Ok(new { message = "Xoá thành công" });
				}
				return BadRequest(new {message = "Xoá thất bại"});
			}catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
