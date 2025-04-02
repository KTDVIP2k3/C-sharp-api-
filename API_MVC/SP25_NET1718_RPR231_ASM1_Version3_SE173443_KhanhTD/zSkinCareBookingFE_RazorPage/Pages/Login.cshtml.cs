using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zSkinCareBookingRepositories_.DTO;

namespace zSkinCareBookingFE_RazorPage.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public LoginDTO LoginRequest { get; set; }
        public void OnGet()
        {
        }
    }
}
