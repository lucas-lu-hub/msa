using LucasNotes.Identity.Controllers.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LucasNotes.Identity.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public string Login(LoginInput input)
        {
            // 验证账号密码

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, input.UserName),
                new Claim(ClaimTypes.Role, "roleName"),
                new Claim(ClaimTypes.Email, "something@lucas.com"),
                new Claim(ClaimTypes.Expired, DateTime.Now.AddYears(2).ToLongTimeString()),
                new Claim("IsAdmin", "true"),
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],     //Issuer
                _configuration["Jwt:Audience"],   //Audience
                claims,                          //Claims,
                DateTime.Now,                    //notBefore
                DateTime.Now.AddYears(2),    //expires
                signingCredentials               //Credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
            //var response = new HttpResponseMessage();

            //var cookie = new CookieHeaderValue("Authorization", $"Bearer {token}");
            //response.Headers.Add("Cookie", new[] { $"Bearer {token}" });
            //Response.Cookies.Append("Bearer", token);
        }
    }
}
