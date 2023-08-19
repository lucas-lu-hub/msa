using CommonLib.Consts;
using CommonLib.Interface;
using Grpc.Net.Client;
using LucasNotes.UserService.Protos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace LucasNotes.UserApi.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        //private const string _userServiceUrl = "https://localhost:7029";
        private readonly IConsulService _consulService;
        private readonly IDistributedCache _distributedCache;

        public UserController(IConsulService consulService,
            IDistributedCache distributedCache)
        {
            _consulService = consulService;
            _distributedCache = distributedCache;
        }

        [Authorize]
        [HttpGet]
        public async Task<UserDto> GetUserById(int id)
        {
            //var user = await _distributedCache.GetStringAsync($"user_{id}");
            //if (!string.IsNullOrWhiteSpace(user))
            //{
            //    var result = user.ToObject<UserCache>();
            //    return new UserDto
            //    {
            //        Email = result.Email,
            //        UserId = result.UserId,
            //        Gender = result.Gender,
            //        UserName = result.UserName
            //    };
            //}

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

        [Authorize]
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
