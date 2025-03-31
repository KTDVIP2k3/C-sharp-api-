using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using zSkinCareBookingRepositories_.DTO;
using zSkinCareBookingRepositories_.Models;
using zSkinCareBookingServices_.InterfaceService;

namespace zSkinCareBookin.ApiService_.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TherapistController : ControllerBase
	{
		private readonly TherapistServiceInterface _therapistInterface;

		public TherapistController(TherapistServiceInterface therapistInterface)
		{
			_therapistInterface = therapistInterface;
		}



		[HttpGet("/GetAllTherapist")]
		[EnableQuery]
		[Authorize]
		public async Task<IActionResult> GetAllTherapist()
		{
			List<Therapist> therapists = await _therapistInterface.GetTherapists();
			if(therapists.IsNullOrEmpty())
			{
				return Ok(new { message = "Danh sách chuyên gia đang trống!", data = therapists, status = HttpStatusCode.NoContent});
			}
			return Ok(new { message = "Danh sách chuyên gia", data = therapists, status = HttpStatusCode.OK });
		}

		[HttpGet("/GetTherapistById{therapistId}")]
		[Authorize]
		public async Task<IActionResult> GetTherapisById(int therapistId)
		{
			try
			{
				var therapist = await _therapistInterface.GetTherapistById(therapistId);
				if (therapist == null)
				{
					return NotFound(new { message = "Id therapist này không tồn tại!", data = therapist, status = HttpStatusCode.NotFound });
				}
				return Ok(new { message = "Therapist", data = therapist, status = HttpStatusCode.OK });
			}catch (Exception ex)
			{
				return StatusCode(500, new {message = "Lỗi hệ thống", error = ex.Message, status = HttpStatusCode.InternalServerError });
			}
		}

		[HttpPost("/CreateTherapist")]
		[Authorize]
		public async Task<IActionResult> CreateTherapist([FromBody] TherapistDTO therapistDTO)
		{
			try
			{
				int result = await _therapistInterface.CreateTherapist(therapistDTO);
				if (result == 0)
				{
					return StatusCode(500, new { message = "Tạo therapist thất bại", data = result, status = HttpStatusCode.InternalServerError });
				}
				return Ok(new { message = "Tạo thành công", data = therapistDTO, status = HttpStatusCode.OK });
			}catch(Exception ex)
			{
				return StatusCode(500, new { message = "Lỗi", data = ex.Message, status = HttpStatusCode.InternalServerError });
			}
		}

		[HttpPut("/UpdateTherapist")]
		[Authorize]
		public async Task<IActionResult> UpdateTherapist(TherapistDTO therapistDTO)
		{
			try
			{
				int result = await _therapistInterface.UpdateTherapist(therapistDTO);
				if (result == 0)
				{
					return StatusCode(500, new { message = "Cập nhật therapist thất bại", data = result, status = HttpStatusCode.InternalServerError });
				}
				return Ok(new { message = "Cập nhật thành công", data = therapistDTO, status = HttpStatusCode.OK });
			}catch(Exception ex)
			{
				return StatusCode(500, new { message = "Lỗi", data = ex.Message, status = HttpStatusCode.InternalServerError });
			}
		}

		[HttpDelete("/DeleteTherapist/{therapistId}")]
		[Authorize]
		public async Task<IActionResult> DeleteTherapistById(int therapistId)
		{
			try
			{
				if (await _therapistInterface.DeleteTheraPistById(therapistId))
				{
					return Ok(new { message = "Xoá therapist thành công" });
				}
				return StatusCode(500, new { message = "Xoá therapist thất bại" });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Lỗi", data = ex.Message, status = HttpStatusCode.InternalServerError });
			}
		}
	}
}
