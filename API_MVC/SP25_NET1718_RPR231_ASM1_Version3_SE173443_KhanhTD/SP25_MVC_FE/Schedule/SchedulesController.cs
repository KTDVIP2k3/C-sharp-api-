using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using zSkinCareBookingRepositories.DTO;
using zSkinCareBookingRepositories_.DBContext;
using zSkinCareBookingRepositories_.Models;

namespace SP25_MVC_FE.Schedule
{
    [Authorize]
    public class SchedulesController : Controller
    {
        private readonly SkincareBookingSystemV2Context _context = new SkincareBookingSystemV2Context();

        private string APIEndPoint = "https://localhost:7061/api/";

        //public SchedulesController(SkincareBookingSystemV2Context context)
        //{
        //    _context = context;
        //}

        // GET: Schedules

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var skincareBookingSystemV2Context = _context.Schedules.Include(s => s.Therapist);
            //return View(await skincareBookingSystemV2Context.ToListAsync());
            try
            {
                using (
                    var httpClient = new HttpClient())
                {
                    var tokenString = Request.Cookies["TokenString"];

                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);

                    using (var response = await httpClient.GetAsync(APIEndPoint + "Schedule/GetAllSchedule"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<JObject>(jsonResponse);
                            if (result.TryGetValue("data", out JToken dataToken))
                            {
                                var schedules = dataToken.ToObject<List<zSkinCareBookingRepositories_.Models.Schedule>>();
                                return View(schedules);
                            }
                            else
                            {
                                // Xử lý khi không tìm thấy trường 'data'
                                return View(new List<zSkinCareBookingRepositories_.Models.Schedule>());
                            }
                        }
                        else
                        {
                            return StatusCode((int)response.StatusCode, "Failed to retrieve schedules");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: Schedules/Details/5

        [HttpGet("Details/{scheduleId}")]
        public async Task<IActionResult> Details(int scheduleId)
        {
            using (var httpClient = new HttpClient())
            {
                var tokenString = Request.Cookies["TokenString"];

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);

                using (var response = await httpClient.GetAsync($"{APIEndPoint}Schedule/GetScheduleById/{scheduleId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<JObject>(jsonResponse);
                        if (result.TryGetValue("data", out JToken dataToken))
                        {
                            var schedule = dataToken.ToObject<zSkinCareBookingRepositories_.Models.Schedule>();
                            return View(schedule);
                        }
                        else
                        {
                            return View(new zSkinCareBookingRepositories_.Models.Schedule());
                            // Xử lý khi không tìm thấy trường 'data'
                            //return View(new List<zSkinCareBookingRepositories_.Models.Schedule>());
                        }
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Failed to retrieve schedules");
                    }
                }
            }

        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id");
            ViewData["TherapistId"] = new SelectList(_context.Therapists, "Id", "Fullname");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(zSkinCareBookingRepositories_.Models.Schedule schedule)
        {
            using (var httpClient = new HttpClient())
            {
                // Lấy token từ cookies hoặc session nếu cần
                var tokenString = Request.Cookies["TokenString"];

                // Thêm header Authorization cho yêu cầu API
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);

                // Cấu hình URL của API để tạo lịch
                var apiUrl = $"{APIEndPoint}Schedule/CreateSchedule"; // Đảm bảo API endpoint đúng

                var scheduleDTO = new ScheduleDTO
                {
                    TherapistId = schedule.TherapistId,
                    BookingId = schedule.BookingId,
                    Date = schedule.Date.ToString(),
                    StartFrom = schedule.StartFrom.ToString(),
                    EndsAt = schedule.EndsAt.ToString()
                };

                // Chuyển đổi đối tượng Schedule thành JSON
                var jsonContent = JsonConvert.SerializeObject(scheduleDTO);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Gửi yêu cầu POST tới API
                using (var response = await httpClient.PostAsync(apiUrl, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // Nếu thành công, trả về thông báo thành công
                        TempData["SuccessMessage"] = "Schedule created successfully!";
                        return RedirectToAction("Index"); // Chuyển hướng về danh sách lịch hoặc trang bạn muốn
                    }
                    else
                    {
                        // Nếu thất bại, trả về lỗi (có thể là thông báo lỗi)
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("", $"Error creating schedule: {errorMessage}");
                        return View(schedule); // Trả về lại form để người dùng sửa lỗi
                    }
                }
            }
        }


        // GET: Schedules/Edit/5

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id", schedule.BookingId);
            ViewData["TherapistId"] = new SelectList(_context.Therapists, "Id", "Fullname", schedule.TherapistId);
            return View(schedule);
        }

        [HttpPost("Edit/{scheduleId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int scheduleId, zSkinCareBookingRepositories_.Models.Schedule schedule)
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
                        return View(schedule);
                    }

                    // Thêm header Authorization cho yêu cầu API
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);

                    // Cấu hình URL của API để cập nhật lịch (có `id` trong đường dẫn)
                    var apiUrl = $"{APIEndPoint}Schedule/UpdateScheduleById/{scheduleId}"; // Đảm bảo API endpoint đúng

                    var scheduleDTO = new ScheduleDTO
                    {
                        TherapistId = schedule.TherapistId,
                        BookingId = schedule.BookingId,
                        Date = schedule.Date.ToString(),
                        StartFrom = schedule.StartFrom.ToString(),
                        EndsAt = schedule.EndsAt.ToString()
                    };

                    // Chuyển đổi đối tượng Schedule thành JSON
                    var jsonContent = JsonConvert.SerializeObject(scheduleDTO);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Gửi yêu cầu PUT tới API (chỉnh sửa lịch)
                    using (var response = await httpClient.PutAsync(apiUrl, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Nếu thành công, trả về thông báo thành công
                            TempData["SuccessMessage"] = "Schedule updated successfully!";
                            return RedirectToAction("Index"); // Chuyển hướng về danh sách lịch hoặc trang bạn muốn
                        }
                        else
                        {
                            // Nếu thất bại, trả về lỗi (có thể là thông báo lỗi)
                            var errorMessage = await response.Content.ReadAsStringAsync();
                            ModelState.AddModelError("", $"Error updating schedule: {errorMessage}");
                            return View(schedule); // Trả về lại form để người dùng sửa lỗi
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Network error: {ex.Message}");
                return View(schedule);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Unexpected error: {ex.Message}");
                return View(schedule);
            }
        }


        // GET: Schedules/Delete/5

        [HttpGet]
        public async Task<IActionResult> Delete(int scheduleId)
        {
            if (scheduleId == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Booking)
                .Include(s => s.Therapist)
                .FirstOrDefaultAsync(m => m.Id == scheduleId);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
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
                        return RedirectToAction("Delete", new { scheduleId = id });
                    }

                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);

                    var apiUrl = $"{APIEndPoint}Schedule/DeleteScheduleById/{id}";
                    var response = await httpClient.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Schedule deleted successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        TempData["ErrorMessage"] = $"Error deleting schedule: {errorMessage}";
                        return RedirectToAction("Delete", new { scheduleId = id });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Unexpected error: {ex.Message}";
                return RedirectToAction("Delete", new { scheduleId = id });
            }
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}
