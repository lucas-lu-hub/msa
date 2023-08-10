using CommonLib;
using CommonLib.Helper;
using LucasNotes.NoteService.Protos.Folder;
using LucasNotes.NoteService.Repositories.Interface;
using System.Data.SqlClient;

namespace LucasNotes.NoteService.Repositories
{

    [Service(Lifetime = ServiceLifetime.Scoped)]
    public class FolderRepository : IFolderRepository
    {
        private readonly DbHelper _dbHelper;

        public FolderRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<bool> AddFolderAsync(FolderDto request)
        {
            var sql = "INSERT INTO [dbo].[Folder] ([Name] ,[ParentId], [UserId]) VALUES(@Name, @ParentId, @UserId)";
            var paramArray = new SqlParameter[]
            {
                new SqlParameter("@Name", request.Name),
                new SqlParameter("@ParentId", request.ParentId),
                new SqlParameter("@UserId", request.UserId)
            };

            return await _dbHelper.ExecuteNonQueryAsync(sql, paramArray) > 0;
        }

        public async Task<bool> HasPermissionAsync(int folderId, int userId)
        {
            var sql = "SELECT 1 FROM [dbo].[Folder] WHERE Id = @Id AND UserId = @UserId ";
            var paramArray = new SqlParameter[]
            {
                new SqlParameter("@Id", folderId),
                new SqlParameter("@UserId", userId)
            };
            var result = await _dbHelper.GetDataSetAsync(sql, paramArray);
            return result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0;
        }

        public async Task<bool> DeleteFolderAsync(int id)
        {
            var sql = "DELETE FROM [dbo].[Folder] WHERE Id = @Id";
            var paramArray = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };
            return await _dbHelper.ExecuteNonQueryAsync(sql, paramArray) > 0; 
        }

        public async Task<List<FolderDto>> GetFoldersAsync(int userId)
        {
            var sql = "SELECT [Id], [Name], [ParentId] FROM [dbo].[Folder] WHERE UserId = @UserId ";
            var paramArray = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId)
            };
            return await _dbHelper.Query<FolderDto>(sql, paramArray);
        }

        public async Task<bool> UpdateFolderNameAsync(UpdateFolderNameRequest request)
        {
            var sql = "UPDATE [dbo].[Folder] SET Name = @Name WHERE Id = @Id";
            var paramArray = new SqlParameter[]
            {
                new SqlParameter("@Id", request.Id),
                new SqlParameter("@Name", request.Name)
            };
            return await _dbHelper.ExecuteNonQueryAsync(sql, paramArray) > 0;
        }

        public async Task<bool> UpdateFolderParentAsync(UpdateFolderParentRequest request)
        {
            var sql = "UPDATE [dbo].[Folder] SET ParentId = @ParentId WHERE Id = @Id";
            var paramArray = new SqlParameter[]
            {
                new SqlParameter("@Id", request.Id),
                new SqlParameter("@ParentId", request.ParentId)
            };
            return await _dbHelper.ExecuteNonQueryAsync(sql, paramArray) > 0;
        }
    }
}
