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

        [HttpPost]
        public async Task Add(UserDto input)
        {
            using (var channel = GrpcChannel.ForAddress(_userServiceUrl))
            {
                var client = new UserManager.UserManagerClient(channel);
                await client.CreateUserAsync(input);
            }
        }

        [HttpGet]
        public async Task<UserDto> GetUserById(int id)
        {
            using (var channel = GrpcChannel.ForAddress(_userServiceUrl))
            {
                var client = new UserManager.UserManagerClient(channel);
                return await client.GetUserByIdAsync(new GetUserByIdRequest
                {
                    Id = id
                });
            }
        }

        [HttpGet]
        public async Task<List<UserDto>> GetUsers()
        {
            using (var channel = GrpcChannel.ForAddress(_userServiceUrl))
            {
                var client = new UserManager.UserManagerClient(channel);
                return (await client.GetUsersAsync(new Google.Protobuf.WellKnownTypes.Empty())).Users.ToList();
            }
        }
    }
}
