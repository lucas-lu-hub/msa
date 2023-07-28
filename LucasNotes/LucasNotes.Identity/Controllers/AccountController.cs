using Grpc.Net.Client;
using LucasNotes.Identity.Controllers.Dto;
using LucasNotes.UserService.Protos;
using Microsoft.AspNetCore.Identity;
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

        private const string _userServiceUrl = "https://localhost:7029";
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<string> Login(LoginInput input)
        {
            // 验证账号密码
            var user = await CheckUserAsync(input.UserName, input.Password);

            if (user == null)
            {
                return null;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Role, "roleName"),
                new Claim(ClaimTypes.Email, user.Email),
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
        }

        private async Task<UserDto> CheckUserAsync(string name, string pwd)
        {
            using (var channel = GrpcChannel.ForAddress(_userServiceUrl))
            {
                var client = new UserManager.UserManagerClient(channel);
                return await client.CheckUserPwdAsync(new CheckUserPwdRequest
                 {
                     Name = name,
                     Pwd = pwd
                 });
            }
        }
    }
}
