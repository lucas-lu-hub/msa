using CommonLib.Consts;
using CommonLib.Interface;
using Grpc.Net.Client;
using LucasNotes.Identity.Controllers.Dto;
using LucasNotes.UserService.Protos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LucasNotes.Identity.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {

        //private const string _userServiceUrl = "https://localhost:7029";
        private readonly IConfiguration _configuration;
        private readonly IConsulService _consulService;

        public AccountController(IConfiguration configuration,
            IConsulService consulService)
        {
            _configuration = configuration;
            _consulService = consulService;
        }

        [HttpPost]
        public async Task<string> Login(LoginInput input)
        {
            // 验证账号密码
            var user = await CheckUserAsync(input.UserName, input.Password);

            if (user?.UserId < 0)
            {
                return null;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Sid, user.UserId.ToString()),
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
            var url = await _consulService.GetUrlFromServiceNameAsync(ServiceNames.UserService);
            using (var channel = GrpcChannel.ForAddress(url))
            {
                var client = new UserManager.UserManagerClient(channel);
                return await client.CheckUserPwdAsync(new CheckUserPwdRequest
                {
                    Name = name,
                    Pwd = pwd
                });
            }
        }

        [HttpPost]
        public async Task Add(UserDto input)
        {
            using (var channel = GrpcChannel.ForAddress(await _consulService.GetUrlFromServiceNameAsync(ServiceNames.UserService)))
            {
                var client = new UserManager.UserManagerClient(channel);
                await client.CreateUserAsync(input);
            }
        }
    }
}
