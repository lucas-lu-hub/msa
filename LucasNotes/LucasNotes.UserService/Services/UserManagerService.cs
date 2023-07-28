using Grpc.Core;
using LucasNotes.UserService.Protos;

namespace LucasNotes.UserService.Services
{
    public class UserManagerService : UserManager.UserManagerBase
    {
        public override async Task<UserDto> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            return await Task.FromResult(new UserDto());
        }

        public override async Task<GetUsersReply> GetUsers(GetUserByIdRequest request, ServerCallContext context)
        {
            var result = new GetUsersReply();
            result.Users.AddRange(new[] { new UserDto() });
            return await Task.FromResult(result);
        }
    }
}
