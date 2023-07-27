using Google.Protobuf.Collections;
using Grpc.Core;
using LucasNotes.UserService.Protos;

namespace LucasNotes.UserService.Services
{
    public class UserManagerService : UserManager.UserManagerBase 
    {
        public override async Task<GetUserByIdReply> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            var users = new RepeatedField<UserDto>
            {
                new UserDto(),
                new UserDto(),
                new UserDto(),
                new UserDto(),
                new UserDto(),
            };
            return new GetUserByIdReply().Users();
        }
    }
}
