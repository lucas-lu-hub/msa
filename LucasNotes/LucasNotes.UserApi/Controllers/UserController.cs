using Grpc.Net.Client;
using LucasNotes.UserApi.Controllers.Dto;
using LucasNotes.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LucasNotes.UserApi.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class UserController : Controller
    {
        [HttpPost]
        public void Add(UserDto input)
        {
            // createUser
        }

        [HttpGet]
        public UserDto GetUserById(int id)
        {
            return new UserDto();
        }

        [HttpGet]
        public List<UserDto> GetUsers()
        {
            return new List<UserDto> { new UserDto() };
        }

        [HttpGet]
        public async Task<string> TestGrpc()
        {
            var url = "https://localhost:7029";
            using (var channel = GrpcChannel.ForAddress(url))
            {
                var client = new Greeter.GreeterClient(channel);
                var reply = await client.SayHelloAsync(new HelloRequest
                {
                    Name = "lucasUser"
                });
                return reply.Message;
            }
        }
    }
}
