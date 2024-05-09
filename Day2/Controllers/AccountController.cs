using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Day2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration configuration;

        public AccountController(IConfiguration config)
        {
            this.configuration = config;
        }

        [HttpPost] 
        public IActionResult Login(string email, string password)
        {

            if (email == null || password == null)
            {
                return BadRequest();
            }

            if (email == "ash@admin.com" && password == "Ash@12345")
            {
                List<Claim> userData = new List<Claim>();
                userData.Add(new Claim(ClaimTypes.Email, email));
                userData.Add(new Claim(ClaimTypes.Role, "admin"));

                string key = configuration.GetSection("SecretKey").Value;

                SecurityKey secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

                SigningCredentials signCred = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: userData,
                    expires: DateTime.Now.AddMinutes(3), 
                    signingCredentials: signCred);

                var sentToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { token = sentToken });



            }

            return Unauthorized();
        }

    }
}
