using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using zSkinCareBookingRepositories_.DBContext;
using zSkinCareBookingRepositories_.Models;
using System.Text;
using zSkinCareBookingRepositories.DTO;
using zSkinCareBookingRepositories_.DTO;
using Microsoft.AspNetCore.Authorization;

namespace SP25_MVC_FE.Therapists
{
    [Authorize]
    public class TherapistsController : Controller
    {
        private readonly SkincareBookingSystemV2Context _context = new SkincareBookingSystemV2Context();

        private string APIEndPoint = "https://localhost:7061/api/";
        //public TherapistsController(SkincareBookingSystemV2Context context)
        //{
        //    _context = context;
        //}


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                using (
                    var httpClient = new HttpClient())
                {
                    var tokenString = Request.Cookies["TokenString"];

                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);

                    using (var response = await httpClient.GetAsync(APIEndPoint + "Therapist/GetAllTherapist"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<JObject>(jsonResponse);
                            if (result.TryGetValue("data", out JToken dataToken))
                            {
                                var schedules = dataToken.ToObject<List<zSkinCareBookingRepositories_.Models.Therapist>>();
                                return View(schedules);
                            }
                            else
                            {
                                // Xử lý khi không tìm thấy trường 'data'
                                return View(new List<zSkinCareBookingRepositories_.Models.Therapist>());
                            }
                        }
                        else
                        {
                            return StatusCode((int)response.StatusCode, "Failed to retrieve therapists");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int therapistId)
        {
            using (var httpClient = new HttpClient())
            {
                var tokenString = Request.Cookies["TokenString"];

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);

                using (var response = await httpClient.GetAsync($"{APIEndPoint}Therapist/GetTherapistById/{therapistId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<JObject>(jsonResponse);
                        if (result.TryGetValue("data", out JToken dataToken))
                        {
                            var schedule = dataToken.ToObject<zSkinCareBookingRepositories_.Models.Therapist>();
                            return View(schedule);
                        }
                        else
                        {
                            return View(new zSkinCareBookingRepositories_.Models.Therapist());
                            // Xử lý khi không tìm thấy trường 'data'
                            //return View(new List<zSkinCareBookingRepositories_.Models.Schedule>());
                        }
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Failed to retrieve therapists");
                    }
                }
            }

        }


        // GET: Therapists/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username");
            return View();
        }

		// POST: Therapists/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(zSkinCareBookingRepositories_.Models.Therapist therapist)
		{
			using (var httpClient = new HttpClient())
			{
				// Lấy token từ cookies hoặc session nếu cần
				var tokenString = Request.Cookies["TokenString"];

				// Thêm header Authorization cho yêu cầu API
				httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);

				// Cấu hình URL của API để tạo lịch
				var apiUrl = $"{APIEndPoint}Therapist/CreateTherapist"; // Đảm bảo API endpoint đúng

				var therapistDTO = new TherapistDTO
				{
					UserId = therapist.UserId,
					Fullname = therapist.Fullname,
					Email = therapist.Email,
					ExpMonth = therapist.ExpMonth,
					Bio = therapist.Bio,
					Phone = therapist.Phone,
					Specialization = therapist.Specialization,
				};

				// Chuyển đổi đối tượng Schedule thành JSON
				var jsonContent = JsonConvert.SerializeObject(therapistDTO);
				var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

				// Gửi yêu cầu POST tới API
				using (var response = await httpClient.PostAsync(apiUrl, content))
				{
					if (response.IsSuccessStatusCode)
					{
						// Nếu thành công, trả về thông báo thành công
						TempData["SuccessMessage"] = "Therapist created successfully!";
						return RedirectToAction("Index"); // Chuyển hướng về danh sách lịch hoặc trang bạn muốn
					}
					else
					{
						// Nếu thất bại, trả về lỗi (có thể là thông báo lỗi)
						var errorMessage = await response.Content.ReadAsStringAsync();
						ModelState.AddModelError("", $"Error creating therapist: {errorMessage}");
						return View(therapist); // Trả về lại form để người dùng sửa lỗi
					}
				}
			}
		}


        // GET: Therapists/Edit/5

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapist = await _context.Therapists.FindAsync(id);
            if (therapist == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username", therapist.UserId);
            return View(therapist);
        }

        // POST: Therapists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( zSkinCareBookingRepositories_.Models.Therapist therapist)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Lấy token từ cookies hoặc session nếu cần
                    var tokenString = Request.Cookies["TokenString"];

                    if (string.IsNullOrEmpty(tokenString))
                    {
                        ModelState.AddModelError("", "Authentication token is missing.");
                        return View(therapist);
                    }

                    // Thêm header Authorization cho yêu cầu API
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);

                    // Cấu hình URL của API để cập nhật lịch (có `id` trong đường dẫn)
                    var apiUrl = $"{APIEndPoint}Therapist/UpdateTherapist"; // Đảm bảo API endpoint đúng

                    var therapistDTO = new TherapistDTO
                    {
                        Id = therapist.Id,
                        UserId = therapist.UserId,
                        Fullname = therapist.Fullname,
                        Email = therapist.Email,
                        ExpMonth = therapist.ExpMonth,
                        Bio = therapist.Bio,
                        Phone = therapist.Phone,
                        Specialization = therapist.Specialization,
                    };

                    // Chuyển đổi đối tượng Schedule thành JSON
                    var jsonContent = JsonConvert.SerializeObject(therapistDTO);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Gửi yêu cầu PUT tới API (chỉnh sửa lịch)
                    using (var response = await httpClient.PutAsync(apiUrl, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Nếu thành công, trả về thông báo thành công
                            TempData["SuccessMessage"] = "Therapist updated successfully!";
                            return RedirectToAction("Index"); // Chuyển hướng về danh sách lịch hoặc trang bạn muốn
                        }
                        else
                        {
                            // Nếu thất bại, trả về lỗi (có thể là thông báo lỗi)
                            var errorMessage = await response.Content.ReadAsStringAsync();
                            ModelState.AddModelError("", $"Error updating therapist: {errorMessage}");
                            return View(therapist); // Trả về lại form để người dùng sửa lỗi
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Network error: {ex.Message}");
                return View(therapist);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Unexpected error: {ex.Message}");
                return View(therapist);
            }
        }

        // GET: Therapists/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int therapistId)
        {
            if (therapistId == null)
            {
                return NotFound();
            }

            var therapist = await _context.Therapists
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == therapistId);
            if (therapist == null)
            {
                return NotFound();
            }

            return View(therapist);
        }

        // POST: Schedules/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var tokenString = Request.Cookies["TokenString"];
                    if (string.IsNullOrEmpty(tokenString))
                    {
                        TempData["ErrorMessage"] = "Authentication token is missing.";
                        return RedirectToAction("Delete", new { therapistId = id });
                    }

                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);

                    var apiUrl = $"{APIEndPoint}Therapist/DeleteTherapist/{id}";
                    var response = await httpClient.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Therapist deleted successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
						var errorMessage = await response.Content.ReadAsStringAsync();
						var statusCode = response.StatusCode;
						TempData["ErrorMessage"] = $"Error deleting therapist: {errorMessage} (Status Code: {statusCode})";
						return RedirectToAction("Delete", new { therapistId = id });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Unexpected error: {ex.Message}";
                return RedirectToAction("Delete", new { therapistId = id });
            }
        }

        private bool TherapistExists(int id)
        {
            return _context.Therapists.Any(e => e.Id == id);
        }
    }
}
