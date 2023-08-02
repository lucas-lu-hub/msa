using CommonLib.Consts;
using CommonLib.Interface;
using Grpc.Net.Client;
using LucasNotes.UserApi.Controllers.Dto;
using LucasNotes.UserService;
using LucasNotes.UserService.Protos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LucasNotes.UserApi.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class UserController : Controller
    {
        private const string _userServiceUrl = "https://localhost:7029";
        private readonly IConsulService _consulService;

        public UserController(IConsulService consulService)
        {
            _consulService = consulService;
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

        [HttpGet]
        public async Task<UserDto> GetUserById(int id)
        {
            using (var channel = GrpcChannel.ForAddress(await _consulService.GetUrlFromServiceNameAsync(ServiceNames.UserService)))
            {
                var client = new UserManager.UserManagerClient(channel);
                var result = await client.GetUserByIdAsync(new GetUserByIdRequest
                {
                    Id = id
                });
                return result.UserId < 0 ? null : result;
            }
        }

        [HttpGet]
        public async Task<List<UserDto>> GetUsers()
        {
            using (var channel = GrpcChannel.ForAddress(await _consulService.GetUrlFromServiceNameAsync(ServiceNames.UserService)))
            {
                var client = new UserManager.UserManagerClient(channel);
                return (await client.GetUsersAsync(new Google.Protobuf.WellKnownTypes.Empty())).Users.ToList();
            }
        }
    }
}
