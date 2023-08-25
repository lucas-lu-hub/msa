using CommonLib;
using CommonLib.Helper;
using LucasNotes.NoteService.Protos.Note;
using LucasNotes.NoteService.Repositories.Interface;
using System.Data.SqlClient;

namespace LucasNotes.NoteService.Repositories
{

    [Service(Lifetime = ServiceLifetime.Scoped)]
    public class NoteRepository : INoteRepository
    {
        private readonly DbHelper _dbHelper;

        public NoteRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<bool> AddNoteAsync(AddNoteRequest request)
        {
            var sql = "INSERT INTO [dbo].[Note] ([UserId],[FolderId],[Name],[Content],[Tag]) " +
                "VALUES( @UserId, @FolderId, @Name, @Content, @Tag)";
            var paramArray = new SqlParameter[]
            {
                new SqlParameter("@UserId", request.UserId),
                new SqlParameter("@FolderId", request.Note.FolderId),
                new SqlParameter("@Name", request.Note.Name),
                new SqlParameter("@Content", request.Note.Content),
                new SqlParameter("@Tag", request.Note.Tag),
            };

            var result = await _dbHelper.ExecuteNonQueryAsync(sql, paramArray);
            return result > 0;
        }

        public async Task<bool> DeleteNotesAsync(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return true;
            }
            var sql = "DELETE FROM [dbo].[Note] WHERE Id in (" + string.Join(",", ids) + " ) ";
            var paramArray = new SqlParameter[] { };

            var result = await _dbHelper.ExecuteNonQueryAsync(sql, paramArray);
            return result > 0;
        }

        public async Task<NoteDto> GetNoteByIdAsync(int id)
        {
            var sql = "SELECT UserId, FolderId, Name, [Content] " +
                "FROM [dbo].[Note] " +
                "WHERE Id = @id ";

            var paramArray = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            var result = await _dbHelper.Query<NoteDto>(sql, paramArray);
            return result.FirstOrDefault();
        }

        public async Task<List<NoteDto>> GetNotesAsync(List<int> ids)
        {
            var sql = "SELECT Id, UserId, FolderId, Name, [Content] " +
                    "FROM [dbo].[Note] ";

            if (ids != null && ids.Count > 0)
            {
                sql += "WHERE Id IN ( " + string.Join(",", ids) + " ) ";
            }  

            var paramArray = new SqlParameter[]{ };

            return await _dbHelper.Query<NoteDto>(sql, paramArray);
        }

        public async Task<List<NoteDto>> GetNotesByFolderIdAsync(int folderId)
        {
            var sql = "SELECT Id, UserId, FolderId, Name, [Content] " +
                    "FROM [dbo].[Note] WHERE FolderId = @folderId ";


            var paramArray = new SqlParameter[]
            {
                new SqlParameter("@folderId", folderId)
            };

            return await _dbHelper.Query<NoteDto>(sql, paramArray);
        }

        public async Task<bool> UpdateNoteAsync(NoteDto note)
        {
            return await UpdateNotesAsync(new List<NoteDto> { note });
        }

        public async Task<bool> UpdateNotesAsync(List<NoteDto> notes)
        {
            var sql = string.Empty;
            var paramArray = new SqlParameter[notes.Count];
            for(int i = 0; i < notes.Count; i++)
            {
                var note = notes[i];
                sql += $" UPDATE [dbo].[Note] SET [FolderId] = {note.FolderId}, [Name] = {note.Name}, [Content] = {note.Content}, [Tag] = {note.Tag} " +
                       $" WHERE Id = @id{i} ";

                paramArray[i] = new SqlParameter($"@id{i}", note.Id);
            }
            var result = await _dbHelper.ExecuteNonQueryAsync(sql, paramArray);
            return result > 0;
        }
    }
}
