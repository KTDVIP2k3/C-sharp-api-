using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using zSkinCareBookingRepositories_.DTO;
using zSkinCareBookingRepositories_.Models;
using zSkinCareBookingServices_.InterfaceService;

namespace zSkinCareBookin.ApiService_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly UserAccountServiceInterface _userAccountServiceInterface;

        public AuthController(IConfiguration config, UserAccountServiceInterface userAccountServiceInterface)
        {
            _config = config;
            _userAccountServiceInterface = userAccountServiceInterface;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO userAccount)
        {
            int result = await _userAccountServiceInterface.Authenticate(userAccount);
            if (result == 0)
            {
                return NotFound(new { message = "UserName khong ton tai", status = HttpStatusCode.NotFound });
            }
            else if (result == 1)
            {
                return BadRequest(new { message = "Password khong dung hoac khong ton tai", status = HttpStatusCode.BadRequest });
            }

            String token = GenerateToken(await _userAccountServiceInterface.GetUserAccount(userAccount));
            return Ok(new {message = "Login thanh cong", data = token, status = HttpStatusCode.OK});
        }


        private String GenerateToken(UserAccount userAccount)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            using(var sha256 = SHA256.Create())
            {
                key = sha256.ComputeHash(key);
            }
            var secreteKey = new SymmetricSecurityKey(key);
            var credential = new SigningCredentials(secreteKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, userAccount.UserName),
                    new Claim(ClaimTypes.Role, userAccount.RoleId.ToString())

                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credential
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
