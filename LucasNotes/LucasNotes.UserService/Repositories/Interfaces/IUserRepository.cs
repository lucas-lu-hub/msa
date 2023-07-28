using LucasNotes.UserService.Protos;

namespace LucasNotes.UserService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<bool> CreateUserAsync(UserDto user);
        Task<UserDto> CheckPwdAsync(string name, string pwd);
    }
}
