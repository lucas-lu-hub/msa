using BCrypt.Net;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using LucasNotes.UserService.Protos;
using LucasNotes.UserService.Repositories.Interfaces;

namespace LucasNotes.UserService.Services
{
    public class UserManagerService : UserManager.UserManagerBase
    {
        private readonly IUserRepository _userRepository;

        public UserManagerService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<UserDto> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            return await _userRepository.GetUserByIdAsync(request.Id);
        }

        public override async Task<GetUsersReply> GetUsers(Empty request, ServerCallContext context)
        {
            var users = await _userRepository.GetUsersAsync();
            var result = new GetUsersReply();
            result.Users.AddRange(users);
            return result;
        }

        public override async Task<Empty> CreateUser(UserDto request, ServerCallContext context)
        {
            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            await _userRepository.CreateUserAsync(request);
            return new Empty();
        }

        public override async Task<UserDto> CheckUserPwd(CheckUserPwdRequest request, ServerCallContext context)
        {
            return await _userRepository.CheckPwdAsync(request.Name, request.Pwd);
        }
    }
}
