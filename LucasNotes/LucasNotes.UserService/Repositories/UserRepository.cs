using CommonLib;
using LucasNotes.UserService.Protos;
using LucasNotes.UserService.Repositories.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace LucasNotes.UserService.Repositories
{
    [Service(Lifetime = ServiceLifetime.Scoped)]
    public class UserRepository : IUserRepository
    {
        private readonly DbHelper _dbHelper;

        public UserRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var sql = "SELECT  " +
                      "[Id] AS UserId," +
                      "[Name] AS UserName," +
                      "[Pwd] AS Password," +
                      "[Email]," +
                      "[Gender] " +
                      "FROM [dbo].[User] ";
            return await _dbHelper.Query<UserDto>(sql);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var sql = "SELECT  " +
                      "[Id] AS UserId," +
                      "[Name] AS UserName," +
                      "[Pwd] AS Password," +
                      "[Email]," +
                      "[Gender] " +
                      "FROM [dbo].[User] " +
                      "WHERE Id = @id";
            var paras = new SqlParameter[] { new SqlParameter("@id", id) };
            var result = await _dbHelper.Query<UserDto>(sql, paras);
            return result.FirstOrDefault() ?? new UserDto
            {
                UserId = -1
            };
        } 

        public async Task<bool> CreateUserAsync(UserDto user)
        {
            var sql = "  INSERT INTO [dbo].[User](     " +
                "[Name], " +
                "[Pwd], " +
                "[Email], " +
                "[Gender]) " +
                "VALUES(@Name, @Pwd,@Email,@Gender)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", user.UserName),
                new SqlParameter("@Pwd", user.Password),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Gender",  user.Gender)
            };
            var result = await _dbHelper.ExecuteNonQueryAsync(sql, parameters);
            return result > 0;
        }

        public async Task<UserDto> CheckPwdAsync(string name, string pwd)
        {
            var sql = "SELECT " +
                      "[Id] AS UserId, " +
                      "[Name] AS UserName, " +
                      "[Pwd] AS [Password], " +
                      "[Email]," +
                      "[Gender] " +
                      "FROM [dbo].[User] " +
                      "WHERE [Name] = @Name AND [Pwd] = @Pwd";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@Pwd", pwd),
            };
            var result = await _dbHelper.Query<UserDto>(sql, parameters);
            return result.FirstOrDefault() ?? new UserDto
            {
                UserId = -1
            };
        }
    }
}
