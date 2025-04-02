using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using zSkinCareBookingRepositories_.DTO;
using System.Text.Json;

namespace SP25_MVC_FE.Controllers
{
    public class AccountController : Controller
    {
        private string APIEndPoint = "https://localhost:7061/api/";

        public IActionResult Index() => RedirectToAction("Login");
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginRequest)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "Auth/Login", loginRequest))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = await response.Content.ReadAsStringAsync();

                            var result = JsonSerializer.Deserialize<JsonElement>(jsonResponse);

                            var tokenString = result.GetProperty("data").GetString(); // Truy xuất token từ trường "data"

                            // Xử lý JWT Token
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var jwtToken = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;

                            if (jwtToken != null)
                            {
                                var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role),
                };

                                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                                Response.Cookies.Append("UserName", userName);
                                Response.Cookies.Append("Role", role);
                                Response.Cookies.Append("TokenString", tokenString);

                               return RedirectToAction("Index", "Home");                         
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ModelState.AddModelError("", "Login failure");
            return View();
        }
    }
}
